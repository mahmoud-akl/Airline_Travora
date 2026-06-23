import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getFlights, createFlight, deleteFlight, updateFlight, updateFlightStatus } from '../services/api';
import { formatEgyptTime, toEgyptDateTimeLocal, toUtcIsoString } from '../utils/dateUtils';
import './AdminPage.css';

function Flights() {
  const [flights, setFlights] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingId, setEditingId] = useState(null);
  const [searchTerm, setSearchTerm] = useState('');
  
  const initialForm = {
    flightNumber: '',
    departureAirport: '',
    arrivalAirport: '',
    departureTimeUtc: '',
    arrivalTimeUtc: '',
    terminal: '',
    gate: '',
    airlineName: 'Egypt Air',
    airlineIcaoCode: 'MS',
    airlineIataCode: 'MS',
    originCity: '',
    destinationCity: '',
    scheduledBoardingTimeUtc: '',
    departureIataCode: '',
    arrivalIataCode: ''
  };

  const [formData, setFormData] = useState(initialForm);
  const navigate = useNavigate();

  useEffect(() => {
    loadFlights();
  }, []);



  const loadFlights = async () => {
    try {
      const response = await getFlights();
      setFlights(response.data.reverse()); // Newest first
    } catch (error) {
      console.error('Error loading flights:', error);
      if (error.response?.status === 401) {
        navigate('/');
      }
    } finally {
      setLoading(false);
    }
  };

  const handleAddClick = () => {
    setEditingId(null);
    setFormData(initialForm);
    setShowForm(!showForm);
  };

  const handleEditClick = (flight) => {
    setEditingId(flight.flightId);
    setFormData({
      flightNumber: flight.flightNumber || '',
      departureAirport: flight.departureAirport || '',
      arrivalAirport: flight.arrivalAirport || '',
      departureTimeUtc: toEgyptDateTimeLocal(flight.departureTimeUtc),
      arrivalTimeUtc: toEgyptDateTimeLocal(flight.arrivalTimeUtc),
      terminal: flight.terminal || '',
      gate: flight.gate || '',
      airlineName: flight.airlineName || 'Egypt Air',
      airlineIcaoCode: flight.airlineIcaoCode || 'MS',
      airlineIataCode: flight.airlineIataCode || 'MS',
      originCity: flight.originCity || '',
      destinationCity: flight.destinationCity || '',
      scheduledBoardingTimeUtc: toEgyptDateTimeLocal(flight.scheduledBoardingTimeUtc),
      departureIataCode: flight.departureIataCode || '',
      arrivalIataCode: flight.arrivalIataCode || ''
    });
    setShowForm(true);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const flightData = {
        ...formData,
        departureTimeUtc: toUtcIsoString(formData.departureTimeUtc),
        arrivalTimeUtc: toUtcIsoString(formData.arrivalTimeUtc),
        scheduledBoardingTimeUtc: toUtcIsoString(formData.scheduledBoardingTimeUtc)
      };
      
      if (editingId) {
        await updateFlight(editingId, flightData);
        alert('Flight updated successfully!');
      } else {
        await createFlight(flightData);
      }
      
      setShowForm(false);
      setFormData(initialForm);
      setEditingId(null);
      loadFlights();
    } catch (error) {
      console.error('Error saving flight:', error);
      alert('Failed to save flight: ' + (error.response?.data?.message || error.message));
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Are you sure you want to delete this flight?')) return;
    try {
      await deleteFlight(id);
      loadFlights();
    } catch (error) {
      alert('Failed to delete flight');
    }
  };

  const handleStatusChange = async (flightId, newStatus) => {
    try {
      await updateFlightStatus(flightId, newStatus);
      loadFlights();
    } catch (error) {
      alert('Failed to update flight status');
    }
  };

  // Filter flights by search term
  const filteredFlights = flights.filter(flight =>
    flight.flightNumber.toLowerCase().includes(searchTerm.toLowerCase()) ||
    flight.departureAirport.toLowerCase().includes(searchTerm.toLowerCase()) ||
    flight.arrivalAirport.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="admin-page">
      <nav className="navbar">
        <h1>✈️ Flight Management</h1>
        <button onClick={() => navigate('/dashboard')} className="back-btn">← Back to Dashboard</button>
      </nav>

      <div className="page-content">
        <div className="page-header">
          <h2>Flights ({filteredFlights.length})</h2>
          <button onClick={handleAddClick} className="add-btn">
            {showForm ? 'Cancel' : '+ Add New Flight'}
          </button>
        </div>

        {/* Search Bar */}
        <div className="add-form" style={{marginBottom: '1.5rem'}}>
          <div className="form-group">
            <label>Search Flights</label>
            <input
              type="text"
              placeholder="🔍 Search by flight number, departure, or arrival..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              style={{width: '100%', padding: '0.75rem 1rem'}}
            />
          </div>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} className="add-form">
            <h3>{editingId ? 'Edit Flight' : 'Add New Flight'}</h3>
            <div className="form-row">
              <div className="form-group">
                <label>Flight Number</label>
                <input
                  type="text"
                  value={formData.flightNumber}
                  onChange={(e) => setFormData({...formData, flightNumber: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Airline Name</label>
                <input
                  type="text"
                  value={formData.airlineName}
                  onChange={(e) => setFormData({...formData, airlineName: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Airline ICAO</label>
                <input
                  type="text"
                  value={formData.airlineIcaoCode}
                  onChange={(e) => setFormData({...formData, airlineIcaoCode: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Airline IATA</label>
                <input
                  type="text"
                  value={formData.airlineIataCode}
                  onChange={(e) => setFormData({...formData, airlineIataCode: e.target.value})}
                  required
                />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Departure Airport</label>
                <input
                  type="text"
                  value={formData.departureAirport}
                  onChange={(e) => setFormData({...formData, departureAirport: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Departure IATA</label>
                <input
                  type="text"
                  value={formData.departureIataCode}
                  onChange={(e) => setFormData({...formData, departureIataCode: e.target.value})}
                  placeholder="e.g. CAI"
                />
              </div>
              <div className="form-group">
                <label>Origin City</label>
                <input
                  type="text"
                  value={formData.originCity}
                  onChange={(e) => setFormData({...formData, originCity: e.target.value})}
                  placeholder="e.g. Cairo"
                />
              </div>
              <div className="form-group">
                <label>Arrival Airport</label>
                <input
                  type="text"
                  value={formData.arrivalAirport}
                  onChange={(e) => setFormData({...formData, arrivalAirport: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Arrival IATA</label>
                <input
                  type="text"
                  value={formData.arrivalIataCode}
                  onChange={(e) => setFormData({...formData, arrivalIataCode: e.target.value})}
                  placeholder="e.g. DXB"
                />
              </div>
              <div className="form-group">
                <label>Destination City</label>
                <input
                  type="text"
                  value={formData.destinationCity}
                  onChange={(e) => setFormData({...formData, destinationCity: e.target.value})}
                  placeholder="e.g. Dubai"
                />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Departure Time</label>
                <input
                  type="datetime-local"
                  value={formData.departureTimeUtc}
                  onChange={(e) => setFormData({...formData, departureTimeUtc: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Arrival Time</label>
                <input
                  type="datetime-local"
                  value={formData.arrivalTimeUtc}
                  onChange={(e) => setFormData({...formData, arrivalTimeUtc: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Boarding Time</label>
                <input
                  type="datetime-local"
                  value={formData.scheduledBoardingTimeUtc}
                  onChange={(e) => setFormData({...formData, scheduledBoardingTimeUtc: e.target.value})}
                />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Terminal</label>
                <input
                  type="text"
                  value={formData.terminal}
                  onChange={(e) => setFormData({...formData, terminal: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Gate</label>
                <input
                  type="text"
                  value={formData.gate}
                  onChange={(e) => setFormData({...formData, gate: e.target.value})}
                  required
                />
              </div>
            </div>
            <button type="submit" className="submit-btn">Save Flight</button>
          </form>
        )}

        {loading ? (
          <div className="loading">Loading...</div>
        ) : (
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>Flight Number</th>
                  <th>Airline</th>
                  <th>Route</th>
                  <th>Departure (Egypt Time)</th>
                  <th>Arrival (Egypt Time)</th>
                  <th>Terminal</th>
                  <th>Gate</th>
                  <th>Status</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filteredFlights.map((flight) => (
                  <tr key={flight.flightId}>
                    <td><strong>{flight.flightNumber}</strong></td>
                    <td>
                      <div>{flight.airlineName || 'N/A'}</div>
                      <small className="text-muted" style={{ fontSize: '0.85rem' }}>IATA: {flight.airlineIataCode || '-'} / ICAO: {flight.airlineIcaoCode || '-'}</small>
                    </td>
                    <td>
                      <div>{flight.departureAirport} {flight.departureIataCode && <span className="badge">{flight.departureIataCode}</span>} {flight.originCity && <small className="text-muted">({flight.originCity})</small>}</div>
                      <div>↓</div>
                      <div>{flight.arrivalAirport} {flight.arrivalIataCode && <span className="badge">{flight.arrivalIataCode}</span>} {flight.destinationCity && <small className="text-muted">({flight.destinationCity})</small>}</div>
                    </td>
                    <td>{formatEgyptTime(flight.departureTimeUtc)}</td>
                    <td>{formatEgyptTime(flight.arrivalTimeUtc)}</td>
                    <td>{flight.terminal}</td>
                    <td>{flight.gate}</td>
                    <td>
                      <span className={`status status-${flight.flightStatus.toLowerCase()}`}>
                        {flight.flightStatus}
                      </span>
                    </td>
                    <td>
                      <div style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
                        <select 
                          value={flight.flightStatus}
                          onChange={(e) => handleStatusChange(flight.flightId, e.target.value)}
                          style={{
                            padding: '0.5rem', 
                            borderRadius: '6px', 
                            border: '1px solid #e2e8f0',
                            backgroundColor: '#f8fafc',
                            cursor: 'pointer',
                            fontSize: '0.85rem',
                            fontWeight: '500',
                            color: '#334155'
                          }}
                        >
                          <option value="Scheduled">Scheduled</option>
                          <option value="Active">Active</option>
                          <option value="Delayed">Delayed</option>
                          <option value="Cancelled">Cancelled</option>
                          <option value="Landed">Landed</option>
                        </select>
                        <div style={{display: 'flex', gap: '0.5rem'}}>
                          <button 
                            onClick={() => handleEditClick(flight)} 
                            style={{ 
                              flex: 1, 
                              padding: '0.4rem', 
                              background: '#3b82f6', 
                              color: 'white', 
                              border: 'none', 
                              borderRadius: '6px', 
                              cursor: 'pointer',
                              display: 'flex',
                              alignItems: 'center',
                              justifyContent: 'center',
                              gap: '0.3rem',
                              fontSize: '0.85rem',
                              transition: 'all 0.2s'
                            }}
                            onMouseOver={(e) => e.currentTarget.style.background = '#2563eb'}
                            onMouseOut={(e) => e.currentTarget.style.background = '#3b82f6'}
                          >
                            ✏️ Edit
                          </button>
                          <button 
                            onClick={() => handleDelete(flight.flightId)} 
                            style={{ 
                              flex: 1, 
                              padding: '0.4rem', 
                              background: '#ef4444', 
                              color: 'white', 
                              border: 'none', 
                              borderRadius: '6px', 
                              cursor: 'pointer',
                              display: 'flex',
                              alignItems: 'center',
                              justifyContent: 'center',
                              gap: '0.3rem',
                              fontSize: '0.85rem',
                              transition: 'all 0.2s'
                            }}
                            onMouseOver={(e) => e.currentTarget.style.background = '#dc2626'}
                            onMouseOut={(e) => e.currentTarget.style.background = '#ef4444'}
                          >
                            🗑️ Delete
                          </button>
                        </div>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}

export default Flights;
