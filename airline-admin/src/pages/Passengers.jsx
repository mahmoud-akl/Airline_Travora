import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getPassengers, createPassenger, updatePassenger, deletePassenger } from '../services/api';
import { formatEgyptDate } from '../utils/dateUtils';
import './AdminPage.css';

function Passengers() {
  const [passengers, setPassengers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingId, setEditingId] = useState(null);
  const [searchTerm, setSearchTerm] = useState('');
  
  const initialForm = {
    firstName: '',
    lastName: '',
    passportNumber: '',
    nationality: '',
    dateOfBirth: '',
    passportExpiryDate: '',
    gender: 'Male'
  };
  
  const [formData, setFormData] = useState(initialForm);
  const navigate = useNavigate();

  useEffect(() => {
    loadPassengers();
  }, []);

  const loadPassengers = async () => {
    try {
      const response = await getPassengers();
      setPassengers(response.data.reverse()); // Newest first
    } catch (error) {
      console.error('Error:', error);
      if (error.response?.status === 401) navigate('/');
    } finally {
      setLoading(false);
    }
  };

  const handleAddClick = () => {
    setEditingId(null);
    setFormData(initialForm);
    setShowForm(!showForm);
  };

  const handleEditClick = (passenger) => {
    setEditingId(passenger.passengerId);
    setFormData({
      firstName: passenger.firstName || '',
      lastName: passenger.lastName || '',
      passportNumber: passenger.passportNumber || '',
      nationality: passenger.nationality || '',
      dateOfBirth: passenger.dateOfBirth ? passenger.dateOfBirth.split('T')[0] : '',
      passportExpiryDate: passenger.passportExpiryDate ? passenger.passportExpiryDate.split('T')[0] : '',
      gender: passenger.gender || 'Male'
    });
    setShowForm(true);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editingId) {
        await updatePassenger(editingId, formData);
        alert('Passenger updated successfully!');
      } else {
        await createPassenger(formData);
      }
      setShowForm(false);
      setFormData(initialForm);
      setEditingId(null);
      loadPassengers();
    } catch (error) {
      alert('Failed to save passenger');
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Are you sure you want to delete this passenger?')) return;
    try {
      await deletePassenger(id);
      loadPassengers();
    } catch (error) {
      alert('Failed to delete passenger');
    }
  };

  // Filter passengers by search term
  const filteredPassengers = passengers.filter(p =>
    p.passportNumber.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="admin-page">
      <nav className="navbar">
        <h1>👥 Passenger Management</h1>
        <button onClick={() => navigate('/dashboard')} className="back-btn">← Back to Dashboard</button>
      </nav>

      <div className="page-content">
        <div className="page-header">
          <h2>Passengers ({filteredPassengers.length})</h2>
          <button onClick={handleAddClick} className="add-btn">
            {showForm ? 'Cancel' : '+ Add New Passenger'}
          </button>
        </div>

        {/* Search Bar */}
        <div className="add-form" style={{marginBottom: '1.5rem'}}>
          <div className="form-group">
            <label>Search by Passport Number</label>
            <input
              type="text"
              placeholder="🔍 Search by passport number..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              style={{width: '100%', padding: '0.75rem 1rem'}}
            />
          </div>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} className="add-form">
            <h3>{editingId ? 'Edit Passenger' : 'Add New Passenger'}</h3>
            <div className="form-row">
              <div className="form-group">
                <label>First Name</label>
                <input type="text" value={formData.firstName} onChange={(e) => setFormData({...formData, firstName: e.target.value})} required />
              </div>
              <div className="form-group">
                <label>Last Name</label>
                <input type="text" value={formData.lastName} onChange={(e) => setFormData({...formData, lastName: e.target.value})} required />
              </div>
              <div className="form-group">
                <label>Passport Number</label>
                <input 
                  type="text" 
                  value={formData.passportNumber} 
                  onChange={(e) => setFormData({...formData, passportNumber: e.target.value})} 
                  required 
                  disabled={!!editingId} // Usually passport number shouldn't change easily or you can allow it
                />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label>Nationality</label>
                <input type="text" value={formData.nationality} onChange={(e) => setFormData({...formData, nationality: e.target.value})} required />
              </div>
              <div className="form-group">
                <label>Date of Birth</label>
                <input 
                  type="date" 
                  value={formData.dateOfBirth} 
                  onChange={(e) => setFormData({...formData, dateOfBirth: e.target.value})} 
                  required 
                  disabled={!!editingId} // Often DOB isn't editable
                />
              </div>
              <div className="form-group">
                <label>Passport Expiry Date</label>
                <input type="date" value={formData.passportExpiryDate} onChange={(e) => setFormData({...formData, passportExpiryDate: e.target.value})} required />
              </div>
              <div className="form-group">
                <label>Gender</label>
                <select value={formData.gender} onChange={(e) => setFormData({...formData, gender: e.target.value})}>
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                </select>
              </div>
            </div>
            <button type="submit" className="submit-btn">{editingId ? 'Save Changes' : 'Save Passenger'}</button>
          </form>
        )}

        {loading ? (
          <div className="loading">Loading...</div>
        ) : (
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Passport Number</th>
                  <th>Nationality</th>
                  <th>Gender</th>
                  <th>Date of Birth</th>
                  <th>Passport Expiry</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filteredPassengers.map((p) => (
                  <tr key={p.passengerId}>
                    <td><strong>{p.firstName} {p.lastName}</strong></td>
                    <td>{p.passportNumber}</td>
                    <td>{p.nationality}</td>
                    <td><span className="badge" style={{ backgroundColor: p.gender === 'Female' ? '#f472b6' : '#3b82f6' }}>{p.gender || 'N/A'}</span></td>
                    <td>{formatEgyptDate(p.dateOfBirth)}</td>
                    <td>{formatEgyptDate(p.passportExpiryDate)}</td>
                    <td>
                      <div style={{display: 'flex', gap: '0.5rem'}}>
                        <button 
                          onClick={() => handleEditClick(p)} 
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
                          onClick={() => handleDelete(p.passengerId)} 
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

export default Passengers;
