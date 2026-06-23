import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getTickets, getFlights, getPassengers, createTicket, updateTicket, deleteTicket, generateBoardingPass } from '../services/api';
import './AdminPage.css';

function Tickets() {
  const [tickets, setTickets] = useState([]);
  const [flights, setFlights] = useState([]);
  const [passengers, setPassengers] = useState([]);
  const [loading, setLoading] = useState(true);
  
  const [showForm, setShowForm] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [editingTicketId, setEditingTicketId] = useState(null);

  const [searchTerm, setSearchTerm] = useState('');
  const [filterFlight, setFilterFlight] = useState('');
  const [filterClass, setFilterClass] = useState('');
  const [filterStatus, setFilterStatus] = useState('');
  
  const [formData, setFormData] = useState({
    flightId: '',
    passengerId: '',
    seatNumber: '',
    travelClass: 'Economy',
    allowedBaggageCount: 1,
    maxAllowedWeight: 23
  });
  
  const navigate = useNavigate();

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [ticketsRes, flightsRes, passengersRes] = await Promise.all([
        getTickets(),
        getFlights(),
        getPassengers()
      ]);
      setTickets(ticketsRes.data.reverse()); // Newest first
      setFlights(flightsRes.data);
      setPassengers(passengersRes.data);
    } catch (error) {
      console.error('Error:', error);
      if (error.response?.status === 401) navigate('/');
    } finally {
      setLoading(false);
    }
  };

  const resetFormData = () => {
    setFormData({
      flightId: '',
      passengerId: '',
      seatNumber: '',
      travelClass: 'Economy',
      allowedBaggageCount: 1,
      maxAllowedWeight: 23
    });
  };

  const handleToggleForm = () => {
    if (showForm) {
      setShowForm(false);
      setIsEditing(false);
      setEditingTicketId(null);
      resetFormData();
    } else {
      setShowForm(true);
    }
  };

  const handleEditClick = (ticket) => {
    setFormData({
      flightId: ticket.flightId || '',
      passengerId: ticket.passengerId || '',
      seatNumber: ticket.seatNumber || '',
      travelClass: ticket.travelClass || 'Economy',
      allowedBaggageCount: ticket.allowedBaggageCount ?? 1,
      maxAllowedWeight: ticket.maxAllowedWeight ?? 23
    });
    setIsEditing(true);
    setEditingTicketId(ticket.ticketId);
    setShowForm(true);
    // Scroll up smoothly to focus the edit form
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const payload = {
        seatNumber: formData.seatNumber,
        travelClass: formData.travelClass,
        allowedBaggageCount: parseInt(formData.allowedBaggageCount) || 0,
        maxAllowedWeight: parseFloat(formData.maxAllowedWeight) || 0.0
      };

      if (isEditing) {
        // Run standard PUT API
        await updateTicket(editingTicketId, payload);
        alert('✅ Ticket updated successfully!');
      } else {
        // Run standard POST API
        await createTicket({
          ...payload,
          flightId: parseInt(formData.flightId),
          passengerId: parseInt(formData.passengerId)
        });
        alert('✅ Ticket created successfully!');
      }

      setShowForm(false);
      setIsEditing(false);
      setEditingTicketId(null);
      resetFormData();
      loadData();
    } catch (error) {
      alert('❌ Operation failed: ' + (error.response?.data?.message || error.message));
    }
  };

  const handleStatusChange = async (ticketId, newStatus) => {
    try {
      await updateTicket(ticketId, { boardingStatus: newStatus });
      loadData();
    } catch (error) {
      alert('Failed to update boarding status');
    }
  };

  const handleGenerateBoardingPass = async (ticketNumber) => {
    try {
      await generateBoardingPass(ticketNumber);
      alert('Boarding pass generated successfully!');
      loadData(); // Reload to showcase the active state check
    } catch (error) {
      alert('Failed to generate boarding pass');
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Are you sure you want to delete this ticket?')) return;
    try {
      await deleteTicket(id);
      loadData();
    } catch (error) {
      alert('Failed to delete ticket');
    }
  };

  // Live client filtering
  const filteredTickets = tickets.filter(ticket => {
    const matchesSearch = ticket.ticketNumber.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesFlight = !filterFlight || ticket.flight?.flightNumber === filterFlight;
    const matchesClass = !filterClass || ticket.travelClass === filterClass;
    const matchesStatus = !filterStatus || ticket.boardingStatus === filterStatus;
    return matchesSearch && matchesFlight && matchesClass && matchesStatus;
  });

  return (
    <div className="admin-page">
      <nav className="navbar">
        <h1>🎫 Ticket Management</h1>
        <button onClick={() => navigate('/dashboard')} className="back-btn">← Back to Dashboard</button>
      </nav>

      <div className="page-content">
        <div className="page-header">
          <h2>Tickets ({filteredTickets.length})</h2>
          <button onClick={handleToggleForm} className="add-btn" style={{ backgroundColor: showForm ? '#ef4444' : '#3b82f6' }}>
            {showForm ? 'Cancel' : '+ Add New Ticket'}
          </button>
        </div>

        {/* Quick Lookup Tools */}
        <div className="add-form" style={{marginBottom: '1.5rem'}}>
          <div className="form-row">
            <div className="form-group">
              <label>Search Ticket Number</label>
              <input
                type="text"
                placeholder="🔍 Search by ticket number..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label>Filter by Flight</label>
              <select value={filterFlight} onChange={(e) => setFilterFlight(e.target.value)}>
                <option value="">All Flights</option>
                {flights.map(f => (
                  <option key={f.flightId} value={f.flightNumber}>{f.flightNumber}</option>
                ))}
              </select>
            </div>
            <div className="form-group">
              <label>Filter by Class</label>
              <select value={filterClass} onChange={(e) => setFilterClass(e.target.value)}>
                <option value="">All Classes</option>
                <option value="Economy">Economy</option>
                <option value="Business">Business</option>
                <option value="FirstClass">First Class</option>
              </select>
            </div>
            <div className="form-group">
              <label>Filter by Status</label>
              <select value={filterStatus} onChange={(e) => setFilterStatus(e.target.value)}>
                <option value="">All Statuses</option>
                <option value="NotBoarded">Not Boarded</option>
                <option value="Boarded">Boarded</option>
              </select>
            </div>
          </div>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} className="add-form" style={{ borderTop: isEditing ? '4px solid #f59e0b' : '4px solid #10b981' }}>
            <h3 style={{ margin: '0 0 1.5rem 0', color: isEditing ? '#d97706' : '#059669' }}>
              {isEditing ? '✏️ Edit Ticket Parameters' : '➕ Register New Ticket'}
            </h3>
            
            <div className="form-row">
              <div className="form-group">
                <label>Select Flight</label>
                <select 
                  value={formData.flightId} 
                  onChange={(e) => setFormData({...formData, flightId: e.target.value})}
                  disabled={isEditing}
                  style={{ opacity: isEditing ? 0.75 : 1, cursor: isEditing ? 'not-allowed' : 'default' }}
                  required
                >
                  <option value="">Select Flight</option>
                  {flights.map(f => (
                    <option key={f.flightId} value={f.flightId}>
                      {f.flightNumber} ({f.departureAirport} → {f.arrivalAirport})
                    </option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label>Select Passenger</label>
                <select 
                  value={formData.passengerId} 
                  onChange={(e) => setFormData({...formData, passengerId: e.target.value})}
                  disabled={isEditing}
                  style={{ opacity: isEditing ? 0.75 : 1, cursor: isEditing ? 'not-allowed' : 'default' }}
                  required
                >
                  <option value="">Select Passenger</option>
                  {passengers.map(p => (
                    <option key={p.passengerId} value={p.passengerId}>
                      {p.firstName} {p.lastName} ({p.passportNumber})
                    </option>
                  ))}
                </select>
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Seat Number</label>
                <input
                  type="text"
                  value={formData.seatNumber}
                  onChange={(e) => setFormData({...formData, seatNumber: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Travel Class</label>
                <select 
                  value={formData.travelClass} 
                  onChange={(e) => setFormData({...formData, travelClass: e.target.value})}
                >
                  <option value="Economy">Economy</option>
                  <option value="Business">Business</option>
                  <option value="FirstClass">First Class</option>
                </select>
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Allowed Bags</label>
                <input
                  type="number"
                  min="0"
                  value={formData.allowedBaggageCount}
                  onChange={(e) => setFormData({...formData, allowedBaggageCount: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Max Weight (Kg)</label>
                <input
                  type="number"
                  step="0.5"
                  min="0"
                  value={formData.maxAllowedWeight}
                  onChange={(e) => setFormData({...formData, maxAllowedWeight: e.target.value})}
                  required
                />
              </div>
            </div>
            <button type="submit" className="submit-btn" style={{ backgroundColor: isEditing ? '#f59e0b' : '#10b981' }}>
              {isEditing ? 'Update Details' : 'Save Ticket'}
            </button>
          </form>
        )}

        {loading ? (
          <div className="loading">Loading...</div>
        ) : (
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>Ticket Number</th>
                  <th>Passenger</th>
                  <th>Flight</th>
                  <th>Seat</th>
                  <th>Class</th>
                  <th>Allowed Bags</th>
                  <th>Max Weight (Kg)</th>
                  <th>Bags Count</th>
                  <th>Total Weight (Kg)</th>
                  <th>Boarding Status</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filteredTickets.map((ticket) => (
                  <tr key={ticket.ticketId}>
                    <td><strong>{ticket.ticketNumber}</strong></td>
                    <td>
                      {ticket.passenger ? `${ticket.passenger.firstName} ${ticket.passenger.lastName}` : '-'}
                    </td>
                    <td>
                      {ticket.flight ? `${ticket.flight.flightNumber} (${ticket.flight.departureAirport} → ${ticket.flight.arrivalAirport})` : '-'}
                    </td>
                    <td>{ticket.seatNumber}</td>
                    <td>{ticket.travelClass}</td>
                    <td>
                      <span className="badge" style={{ backgroundColor: '#e0f2fe', color: '#0369a1' }}>
                        {ticket.allowedBaggageCount}
                      </span>
                    </td>
                    <td>
                      <span className="badge" style={{ backgroundColor: '#fce7f3', color: '#be185d' }}>
                        {ticket.maxAllowedWeight} Kg
                      </span>
                    </td>
                    <td>
                      <span className="badge" style={{ backgroundColor: '#f1f5f9', color: '#475569' }}>
                        {ticket.baggageCount || 0}
                      </span>
                    </td>
                    <td>
                      <span className="badge" style={{ backgroundColor: '#fef3c7', color: '#d97706' }}>
                        {ticket.totalBaggageWeight || 0} Kg
                      </span>
                    </td>
                    <td>
                      <select 
                        value={ticket.boardingStatus}
                        onChange={(e) => handleStatusChange(ticket.ticketId, e.target.value)}
                        style={{
                          padding: '0.4rem', 
                          borderRadius: '6px', 
                          border: '1px solid #e2e8f0',
                          backgroundColor: '#f8fafc',
                          cursor: 'pointer',
                          fontSize: '0.85rem',
                          fontWeight: '500',
                          color: '#334155'
                        }}
                      >
                        <option value="NotBoarded">Not Boarded</option>
                        <option value="Boarded">Boarded</option>
                      </select>
                    </td>
                    <td>
                      <div className="action-buttons" style={{gap: '0.4rem', display: 'flex', flexWrap: 'wrap'}}>
                        <button 
                          onClick={() => handleGenerateBoardingPass(ticket.ticketNumber)}
                          className={`action-btn ${ticket.barcodeData ? 'success' : ''}`}
                          disabled={ticket.barcodeData}
                          style={{flex: '1', minWidth: '150px'}}
                        >
                          {ticket.barcodeData ? '✓ Boarding Pass' : 'Generate Boarding Pass'}
                        </button>
                        
                        <button 
                          onClick={() => handleEditClick(ticket)}
                          style={{ 
                            flex: '1', 
                            padding: '0.4rem 0.8rem', 
                            background: '#f59e0b', 
                            color: 'white', 
                            border: 'none', 
                            borderRadius: '6px', 
                            cursor: 'pointer',
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'center',
                            gap: '0.3rem',
                            fontSize: '0.85rem',
                            transition: 'all 0.2s',
                            minWidth: '75px'
                          }}
                          onMouseOver={(e) => e.currentTarget.style.background = '#d97706'}
                          onMouseOut={(e) => e.currentTarget.style.background = '#f59e0b'}
                        >
                          ✏️ Edit
                        </button>

                        <button 
                          onClick={() => handleDelete(ticket.ticketId)}
                          style={{ 
                            flex: '1', 
                            padding: '0.4rem 0.8rem', 
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
                            transition: 'all 0.2s',
                            minWidth: '75px'
                          }}
                          onMouseOver={(e) => e.currentTarget.style.background = '#dc2626'}
                          onMouseOut={(e) => e.currentTarget.style.background = '#ef4444'}
                        >
                          🗑️ Delete
                        </button>
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

export default Tickets;
