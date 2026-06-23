import { useState, useEffect } from 'react';
import { 
  generateBaggageTags, 
  getBaggageByTicket, 
  updateBaggageWeight, 
  updateBaggageLocation, 
  updateAllBaggageByTicket, 
  deleteBaggageTag, 
  deleteAllBaggageByTicket,
  getTicketBaggageAllowance
} from '../services/api';
import { formatEgyptTime } from '../utils/dateUtils';
import './BaggageDrop.css';

const LOCATIONS = [
  'Security Check',
  'Terminal',
  'Gate',
  'Loaded on Aircraft',
  'Arrived at Dest',
  'Customs',
  'Baggage Belt'
];

export default function BaggageDrop() {
  const [ticketNumber, setTicketNumber] = useState('');
  const [pendingWeights, setPendingWeights] = useState([]);
  const [allowance, setAllowance] = useState(null);
  
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);

  const [ticketData, setTicketData] = useState(null);
  
  // Tracking & actions
  const [bulkLocation, setBulkLocation] = useState('');
  const [bulkUpdating, setBulkUpdating] = useState(false);
  const [updating, setUpdating] = useState({});

  // Reactive derived values
  const existingBags = ticketData?.bags || [];
  const existingBagsCount = existingBags.length;
  const allowedCount = allowance?.allowedBaggageCount || 0;
  const remainingCount = Math.max(0, allowedCount - existingBagsCount);
  const existingWeightTotal = existingBags.reduce((acc, cur) => acc + (cur.weightKg || 0), 0);

  // Keep weight input array synced dynamically with current remaining allowance
  useEffect(() => {
    if (allowance) {
      // Set correct array size filled with empty strings
      setPendingWeights(Array(remainingCount).fill(''));
    } else {
      setPendingWeights([]);
    }
  }, [remainingCount, allowance !== null]);

  const handlePendingWeightChange = (index, value) => {
    const newWeights = [...pendingWeights];
    newWeights[index] = value;
    setPendingWeights(newWeights);
  };

  const handleGenerateAndSave = async (e) => {
    e.preventDefault();
    if (!ticketNumber || !allowance) return;

    // Filter non-empty, valid positive weights entered by agent
    const validWeights = pendingWeights
      .map(w => parseFloat(w))
      .filter(w => !isNaN(w) && w > 0);

    if (validWeights.length === 0) {
      setError('Please enter weight for at least one bag.');
      return;
    }

    const inputBagsCount = validWeights.length;
    const newBagsWeightTotal = validWeights.reduce((acc, cur) => acc + cur, 0);
    
    const totalBagsCount = existingBagsCount + inputBagsCount;
    const totalWeight = existingWeightTotal + newBagsWeightTotal;

    // Capacity threshold checks
    if (totalBagsCount > allowance.allowedBaggageCount) {
      setError(`⚠️ Cannot exceed bag count allowance! Allowed: ${allowance.allowedBaggageCount}. Registered: ${existingBagsCount}. Attempting to add: ${inputBagsCount}.`);
      return;
    }

    if (totalWeight > allowance.maxAllowedWeight) {
      setError(`⚠️ Weight limit exceeded! Allowed: ${allowance.maxAllowedWeight} Kg. Already dropped: ${existingWeightTotal.toFixed(1)} Kg. Attempting to add: ${newBagsWeightTotal.toFixed(1)} Kg. Total: ${totalWeight.toFixed(1)} Kg.`);
      return;
    }

    setLoading(true);
    setError(null);
    setSuccess(false);
    
    try {
      // 1. Generate baggage tags for JUST the filled count of bags
      const response = await generateBaggageTags(ticketNumber, inputBagsCount);
      const newTags = response.data.tags;
      
      // 2. Concurrently update weights for generated tags
      const promises = newTags.map((tag, i) => {
        const weight = validWeights[i];
        return updateBaggageWeight(tag.tagNumber, weight);
      });
      
      await Promise.all(promises);
      
      setSuccess(true);
      // Wait for latest data refresh - derived effects will auto-adjust remaining tabs!
      await fetchTicketDetails(ticketNumber);
      
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to generate and save baggage tags');
    } finally {
      setLoading(false);
    }
  };

  const fetchTicketDetails = async (ticketNum) => {
    try {
      const res = await getBaggageByTicket(ticketNum);
      setTicketData(res.data);
    } catch (err) {
      if (err.response && err.response.status === 404) {
         setError('Baggage retrieval endpoint returned an error.');
      } else {
         setError('Could not fetch latest baggage tracking state');
      }
    }
  };

  const handleTicketSearch = async (e) => {
    e.preventDefault();
    if (!ticketNumber) return;
    
    setLoading(true);
    setError(null);
    setSuccess(false);
    setAllowance(null);
    setTicketData(null);

    try {
      // 1. Pull constraints FIRST
      const allowanceRes = await getTicketBaggageAllowance(ticketNumber);
      const allowanceData = allowanceRes.data;
      setAllowance(allowanceData);
      
      // 2. Pull dropped bags list & passenger overview
      const ticketRes = await getBaggageByTicket(ticketNumber);
      setTicketData(ticketRes.data);

    } catch (err) {
      setError(err.response?.data?.message || 'Ticket not found. Please verify ticket number.');
    } finally {
      setLoading(false);
    }
  };

  // Location Updates
  const handleUpdateAll = async () => {
    if (!bulkLocation) {
      alert('Please select a location first');
      return;
    }
    setBulkUpdating(true);
    try {
      await updateAllBaggageByTicket(ticketNumber, { location: bulkLocation });
      await fetchTicketDetails(ticketNumber);
      alert('✅ All bags updated successfully!');
      setBulkLocation('');
    } catch (err) {
      alert('❌ Failed to update: ' + (err.response?.data?.message || err.message));
    } finally {
      setBulkUpdating(false);
    }
  };

  const handleUpdateSingle = async (tagNumber) => {
    const sel = document.getElementById('loc-' + tagNumber);
    const location = sel?.value;
    if (!location) {
      alert('Please select a location first');
      return;
    }
    
    setUpdating(prev => ({ ...prev, [tagNumber]: true }));
    try {
      await updateBaggageLocation(tagNumber, { location });
      await fetchTicketDetails(ticketNumber);
      alert('✅ Bag ' + tagNumber + ' updated to: ' + location);
    } catch (err) {
      alert('❌ Failed to update: ' + (err.response?.data?.message || err.message));
    } finally {
      setUpdating(prev => ({ ...prev, [tagNumber]: false }));
    }
  };

  // Delete Handlers
  const handleDeleteSingle = async (tagNumber) => {
    if (!window.confirm('Are you sure you want to delete this bag?')) return;
    
    setUpdating(prev => ({ ...prev, [tagNumber]: true }));
    try {
      await deleteBaggageTag(tagNumber);
      await fetchTicketDetails(ticketNumber);
    } catch (err) {
      alert('❌ Failed to delete: ' + (err.response?.data?.message || err.message));
    } finally {
      setUpdating(prev => ({ ...prev, [tagNumber]: false }));
    }
  };

  const handleDeleteAll = async () => {
    if (!window.confirm('Are you sure you want to delete ALL bags for this ticket? This cannot be undone.')) return;
    
    setBulkUpdating(true);
    try {
      await deleteAllBaggageByTicket(ticketNumber);
      setTicketData({ ...ticketData, bags: [] }); // Optimistic cleanup
      alert('✅ All bags deleted successfully');
    } catch (err) {
      alert('❌ Failed to delete all bags: ' + (err.response?.data?.message || err.message));
    } finally {
      setBulkUpdating(false);
    }
  };

  // Edit Weight Handler (validates against capacity)
  const handleEditWeight = async (tagNumber, currentWeight) => {
    const newWeightStr = window.prompt(`Enter new weight for bag ${tagNumber}:`, currentWeight);
    if (newWeightStr === null) return; // User cancelled
    
    const newWeight = parseFloat(newWeightStr);
    if (isNaN(newWeight) || newWeight <= 0) {
      alert('Please enter a valid weight.');
      return;
    }

    if (allowance) {
      const cleanWeightTotal = existingBags.reduce((acc, cur) => {
        if (cur.tagNumber === tagNumber) return acc; // Ignore old weight
        return acc + (cur.weightKg || 0);
      }, 0);

      const newTotal = cleanWeightTotal + newWeight;
      if (newTotal > allowance.maxAllowedWeight) {
        alert(`❌ Weight limit exceeded! Max: ${allowance.maxAllowedWeight} Kg. Setting this would yield total weight of ${newTotal.toFixed(1)} Kg.`);
        return;
      }
    }

    setUpdating(prev => ({ ...prev, [tagNumber]: true }));
    try {
      await updateBaggageWeight(tagNumber, newWeight);
      await fetchTicketDetails(ticketNumber);
      alert('✅ Weight updated successfully');
    } catch (err) {
      alert('❌ Failed to update weight: ' + (err.response?.data?.message || err.message));
    } finally {
      setUpdating(prev => ({ ...prev, [tagNumber]: false }));
    }
  };

  return (
    <div className="baggage-drop-container">
      <div className="baggage-drop-header">
        <h1>Baggage Drop & Tracking</h1>
        <p>Register new baggage weights and update live tracking locations.</p>
      </div>

      <div className="baggage-drop-content">
        {/* Left column - Registration flow */}
        <div className="baggage-form-card">
          <h3>Issue New Baggage</h3>
          {error && <div className="error-alert">{error}</div>}
          {success && <div className="success-alert">✅ Baggage generated and saved successfully!</div>}
          
          <form className="drop-form" onSubmit={handleTicketSearch}>
            <div className="form-group">
              <label>Ticket Number</label>
              <div className="input-group">
                <input
                  type="text"
                  placeholder="e.g. TKT6391411801748"
                  value={ticketNumber}
                  onChange={(e) => setTicketNumber(e.target.value)}
                  required
                />
                <button type="submit" className="btn-secondary" disabled={loading}>
                  {loading ? 'Searching...' : 'Search'}
                </button>
              </div>
            </div>

            {allowance && (
              <div style={{
                background: '#eff6ff',
                border: '1px solid #bfdbfe',
                borderRadius: '12px',
                padding: '1rem 1.25rem',
                marginBottom: '1.5rem',
                display: 'grid',
                gridTemplateColumns: '1fr 1fr',
                gap: '1rem',
                boxShadow: '0 2px 4px rgba(59, 130, 246, 0.05)'
              }}>
                <div>
                  <span style={{ display: 'block', fontSize: '0.75rem', color: '#1d4ed8', textTransform: 'uppercase', fontWeight: 'bold', letterSpacing: '0.5px', marginBottom: '0.25rem' }}>
                    Allowed Bags
                  </span>
                  <strong style={{ fontSize: '1.35rem', color: '#1e3a8a', display: 'block' }}>
                    {allowance.allowedBaggageCount} Bags
                  </strong>
                </div>
                <div>
                  <span style={{ display: 'block', fontSize: '0.75rem', color: '#1d4ed8', textTransform: 'uppercase', fontWeight: 'bold', letterSpacing: '0.5px', marginBottom: '0.25rem' }}>
                    Max Allowed Weight
                  </span>
                  <strong style={{ fontSize: '1.35rem', color: '#1e3a8a', display: 'block' }}>
                    {allowance.maxAllowedWeight} Kg
                  </strong>
                </div>
              </div>
            )}

            {allowance && (
              <div className="form-group">
                <label>Remaining Bags Allowed</label>
                <input
                  type="number"
                  value={remainingCount}
                  disabled
                  readOnly
                  style={{ backgroundColor: '#f1f5f9', color: '#334155', opacity: 0.9, cursor: 'not-allowed', fontWeight: 'bold' }}
                />
                <span style={{ fontSize: '0.75rem', color: '#64748b', marginTop: '0.4rem', display: 'inline-block' }}>
                  (Dynamic capacity: Total {allowance.allowedBaggageCount} - Already Dropped {existingBagsCount})
                </span>
              </div>
            )}

            {/* Dynamic Weights Input / Auto-Hide when limit reached */}
            {allowance && (
              remainingCount > 0 ? (
                <>
                  <div className="tags-weight-section">
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1rem' }}>
                      <h4 style={{ margin: 0 }}>Enter Baggage Weights</h4>
                      <span style={{ fontSize: '0.75rem', color: '#2563eb', fontWeight: '500' }}>
                        (Fill up to {remainingCount} available slot{remainingCount > 1 ? 's' : ''})
                      </span>
                    </div>
                    <div className="tags-list">
                      {pendingWeights.map((weight, index) => (
                        <div key={index} className="tag-weight-item">
                          <div className="tag-info">
                            <span className="tag-badge">Bag {existingBagsCount + index + 1}</span>
                          </div>
                          <div className="weight-input">
                            <input
                              type="number"
                              placeholder="Weight"
                              min="0"
                              step="0.1"
                              value={weight}
                              onChange={(e) => handlePendingWeightChange(index, e.target.value)}
                            />
                            <span className="unit">KG</span>
                          </div>
                        </div>
                      ))}
                    </div>
                  </div>
                  
                  <button 
                    type="button" 
                    className="btn-primary" 
                    onClick={handleGenerateAndSave}
                    disabled={loading}
                    style={{ marginTop: '1.5rem' }}
                  >
                    {loading ? 'Processing...' : 'Generate & Save Tags'}
                  </button>
                </>
              ) : (
                <div style={{
                  background: '#ecfdf5',
                  border: '1px solid #6ee7b7',
                  color: '#065f46',
                  borderRadius: '12px',
                  padding: '1.5rem',
                  textAlign: 'center',
                  marginTop: '2rem',
                  boxShadow: '0 4px 6px -1px rgba(0, 0, 0, 0.05)'
                }}>
                  <div style={{ fontSize: '2rem', marginBottom: '0.5rem' }}>🎉</div>
                  <h4 style={{ margin: '0 0 0.5rem 0', fontSize: '1.1rem', fontWeight: '700' }}>Fully Dropped!</h4>
                  <p style={{ margin: 0, fontSize: '0.85rem', color: '#047857', lineHeight: '1.4' }}>
                    All {allowance.allowedBaggageCount} allowed baggage slots have been successfully registered for this ticket.
                  </p>
                </div>
              )
            )}
          </form>
        </div>

        {/* Right column - Passenger & Dropped State */}
        {ticketData && (
          <div className="ticket-details-card" style={{ maxWidth: '100%', overflowX: 'auto' }}>
            <h3>Ticket & Passenger Overview</h3>
            
            <div className="ticket-summary">
              <div className="ticket-main-info" style={{ flexDirection: 'row', justifyContent: 'space-between' }}>
                <div>
                  <span className="passenger-name" style={{ display: 'block', fontSize: '0.9rem', marginBottom: '0.25rem' }}>Passenger</span>
                  <span className="tkt-number" style={{ fontSize: '1.25rem' }}>{ticketData.passengerName}</span>
                </div>
                <div style={{ textAlign: 'right' }}>
                  <span className="passenger-name" style={{ display: 'block', fontSize: '0.9rem', marginBottom: '0.25rem' }}>Flight</span>
                  <span className="tkt-number" style={{ fontSize: '1.25rem' }}>✈️ {ticketData.flightNumber}</span>
                </div>
              </div>
              
              <div className="ticket-stats">
                <div className="stat">
                  <span className="label">Dropped / Max Bags</span>
                  <span className="value" style={{ color: remainingCount === 0 ? '#34d399' : '#38bdf8' }}>
                    🧳 {existingBagsCount} {allowance ? `/ ${allowance.allowedBaggageCount}` : ''}
                  </span>
                </div>
                <div className="stat">
                  <span className="label">Dropped Weight</span>
                  <span className="value" style={{ color: allowance && existingWeightTotal >= allowance.maxAllowedWeight ? '#fb7185' : '#38bdf8' }}>
                    {existingWeightTotal.toFixed(1)} {allowance ? `/ ${allowance.maxAllowedWeight}` : ''} KG
                  </span>
                </div>
                <div className="stat">
                  <span className="label">Passport</span>
                  <span className="value" style={{ color: '#e2e8f0', fontSize: '1.2rem' }}>
                    {ticketData.passportNumber}
                  </span>
                </div>
              </div>
            </div>

            {existingBagsCount > 0 && (
              <div className="bulk-update-box">
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '0.75rem' }}>
                  <h4 style={{ margin: 0 }}>🔄 Update All Bags Location</h4>
                  <button 
                    onClick={handleDeleteAll} 
                    className="btn-danger small-btn" 
                    disabled={bulkUpdating}
                    style={{ background: '#fee2e2', color: '#991b1b', border: '1px solid #fecaca', cursor: 'pointer' }}
                  >
                    🗑️ Delete All Bags
                  </button>
                </div>
                <div className="bulk-update-controls">
                  <select
                    value={bulkLocation}
                    onChange={(e) => setBulkLocation(e.target.value)}
                    className="location-select"
                  >
                    <option value="">-- Select Location --</option>
                    {LOCATIONS.map(loc => (
                      <option key={loc} value={loc}>{loc}</option>
                    ))}
                  </select>
                  <button
                    onClick={handleUpdateAll}
                    disabled={bulkUpdating || !bulkLocation}
                    className="btn-success"
                    style={{ margin: 0, width: 'auto' }}
                  >
                    {bulkUpdating ? 'Updating...' : 'Update All'}
                  </button>
                </div>
              </div>
            )}

            {existingBagsCount > 0 ? (
              <div className="ticket-bags-table-wrapper">
                <table className="bags-table">
                  <thead>
                    <tr>
                      <th>Tag Number</th>
                      <th>Weight (KG)</th>
                      <th>Location</th>
                      <th>Last Update</th>
                      <th>Update Loc</th>
                      <th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    {existingBags.map(bag => (
                      <tr key={bag.tagNumber}>
                        <td className="tag-col">{bag.tagNumber}</td>
                        <td className="weight-col">
                          {bag.weightKg?.toFixed(1) || '0.0'}
                        </td>
                        <td>
                          <span className="location-badge">
                            {bag.currentLocation || '-'}
                          </span>
                        </td>
                        <td style={{ fontSize: '0.85rem', color: '#64748b', whiteSpace: 'nowrap' }}>
                          {formatEgyptTime(bag.lastLocationUpdatedAt || bag.createdAt)}
                        </td>
                        <td>
                          <div className="row-actions">
                            <select
                              id={'loc-' + bag.tagNumber}
                              defaultValue=""
                              className="location-select-small"
                            >
                              <option value="">-- Select --</option>
                              {LOCATIONS.map(loc => (
                                <option key={loc} value={loc}>{loc}</option>
                              ))}
                            </select>
                            <button
                              onClick={() => handleUpdateSingle(bag.tagNumber)}
                              disabled={updating[bag.tagNumber]}
                              className="btn-secondary small-btn"
                            >
                              Save
                            </button>
                          </div>
                        </td>
                        <td>
                          <div className="row-actions">
                            <button
                               onClick={() => handleEditWeight(bag.tagNumber, bag.weightKg)}
                               disabled={updating[bag.tagNumber]}
                               className="btn-secondary small-btn"
                               title="Edit Weight"
                            >
                               ✏️
                            </button>
                            <button
                              onClick={() => handleDeleteSingle(bag.tagNumber)}
                              disabled={updating[bag.tagNumber]}
                              className="btn-secondary small-btn"
                              style={{ background: '#fee2e2', color: '#991b1b', cursor: 'pointer' }}
                              title="Delete Bag"
                            >
                              🗑️
                            </button>
                          </div>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            ) : (
              <div className="no-bags-msg">No baggage registered for this ticket yet.</div>
            )}
          </div>
        )}
      </div>
    </div>
  );
}
