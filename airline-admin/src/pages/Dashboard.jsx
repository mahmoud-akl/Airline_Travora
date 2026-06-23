import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getFlights, getPassengers, getTickets } from '../services/api';
import './Dashboard.css';

function Dashboard() {
  const [stats, setStats] = useState({
    flights: 0,
    passengers: 0,
    tickets: 0,
    loading: true
  });
  const navigate = useNavigate();

  useEffect(() => {
    loadStats();
  }, []);

  const loadStats = async () => {
    try {
      const [flightsRes, passengersRes, ticketsRes] = await Promise.all([
        getFlights(),
        getPassengers(),
        getTickets()
      ]);
      setStats({
        flights: flightsRes.data.length,
        passengers: passengersRes.data.length,
        tickets: ticketsRes.data.length,
        loading: false
      });
    } catch (error) {
      console.error('Error loading stats:', error);
      setStats(prev => ({ ...prev, loading: false }));
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/');
  };

  return (
    <div className="dashboard">
      <nav className="navbar">
        <button onClick={handleLogout} className="logout-btn">Logout</button>
        <h1>✈️ Airline Simulation Admin</h1>
      </nav>

      <div className="dashboard-content">
        <h2>Dashboard</h2>
        
        {/* Statistics Cards */}
        <div className="stats-grid">
          <div className="stat-card">
            <div className="stat-icon">✈️</div>
            <div className="stat-info">
              <h3>{stats.loading ? '...' : stats.flights}</h3>
              <p>Total Flights</p>
            </div>
          </div>
          <div className="stat-card">
            <div className="stat-icon">👥</div>
            <div className="stat-info">
              <h3>{stats.loading ? '...' : stats.passengers}</h3>
              <p>Total Passengers</p>
            </div>
          </div>
          <div className="stat-card">
            <div className="stat-icon">🎫</div>
            <div className="stat-info">
              <h3>{stats.loading ? '...' : stats.tickets}</h3>
              <p>Total Tickets</p>
            </div>
          </div>
        </div>

        {/* Main Cards */}
        <div className="cards-grid">
          <div className="card" onClick={() => navigate('/flights')}>
            <div className="card-icon">✈️</div>
            <h3>Flights</h3>
            <p>Manage flight schedules</p>
          </div>
          <div className="card" onClick={() => navigate('/passengers')}>
            <div className="card-icon">👥</div>
            <h3>Passengers</h3>
            <p>Manage passenger information</p>
          </div>
          <div className="card" onClick={() => navigate('/tickets')}>
            <div className="card-icon">🎫</div>
            <h3>Tickets</h3>
            <p>Manage tickets & boarding</p>
          </div>
          
          {/* Customs Management Cards */}
          <div className="card" onClick={() => navigate('/categories')}>
            <div className="card-icon">📦</div>
            <h3>Categories</h3>
            <p>Manage product categories</p>
          </div>
          <div className="card" onClick={() => navigate('/subcategories')}>
            <div className="card-icon">📂</div>
            <h3>Sub Categories</h3>
            <p>Manage sub categories</p>
          </div>
          <div className="card" onClick={() => navigate('/products-customs')}>
            <div className="card-icon">🏷️</div>
            <h3>Products</h3>
            <p>Manage customs rates</p>
          </div>
          <div className="card" onClick={() => navigate('/baggage-drop')}>
            <div className="card-icon">⚖️</div>
            <h3>Baggage Drop</h3>
            <p>Register baggage weights</p>
          </div>
        </div>

        <div className="system-info">
          <h3>📊 System Information</h3>
          <div className="info-grid">
            
            <div className="info-item">
              <span className="value">http://travora-airline.runasp.net</span>
              <span className="label">API URL</span>
            </div>
            {/* <div className="info-item">
              <span className="value">AirlineSimulationDb</span>
              <span className="label">Database</span>
            </div> */}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
