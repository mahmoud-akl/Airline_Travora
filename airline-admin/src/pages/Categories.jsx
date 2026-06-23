import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getCategories, createCategory, deleteCategory } from '../services/api';
import './Dashboard.css';
import './AdminPage.css';

function Categories({ onLogout }) {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({ name: '' });
  const navigate = useNavigate();

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    try {
      const response = await getCategories();
      setCategories(response.data);
      setLoading(false);
    } catch (error) {
      console.error('Error loading categories:', error);
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createCategory(formData);
      setShowForm(false);
      setFormData({ name: '' });
      loadCategories();
      alert('Category created successfully!');
    } catch (error) {
      alert('Failed to create category');
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Are you sure you want to delete this category?')) return;
    try {
      await deleteCategory(id);
      loadCategories();
      alert('Category deleted successfully!');
    } catch (error) {
      alert('Failed to delete category');
    }
  };

  const handleLogout = () => {
    onLogout();
    navigate('/login');
  };

  return (
    <div className="admin-page">
      <div className="navbar">
        <h1>Customs Management System</h1>
        <button onClick={handleLogout} className="logout-btn">Logout</button>
      </div>

      <div className="dashboard-content">
        <div className="page-header">
          <h2>Categories</h2>
          <div className="header-actions">
            <button onClick={() => navigate('/dashboard')} className="btn btn-secondary">
              ← Back to Dashboard
            </button>
            <button onClick={() => setShowForm(!showForm)} className="btn btn-primary">
              {showForm ? 'Cancel' : '+ Add Category'}
            </button>
          </div>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} className="add-form">
            <div className="form-row">
              <div className="form-group" style={{flex: 1}}>
                <label>Category Name</label>
                <input
                  type="text"
                  value={formData.name}
                  onChange={(e) => setFormData({...formData, name: e.target.value})}
                  placeholder="e.g., Electronics, Clothing, Food"
                  required
                />
              </div>
            </div>
            <button type="submit" className="submit-btn">Save Category</button>
          </form>
        )}

        {loading ? (
          <div className="loading">Loading...</div>
        ) : (
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th style={{width: '80px'}}>ID</th>
                  <th>Name</th>
                  <th style={{width: '200px'}}>Sub Categories</th>
                  <th style={{width: '150px'}}>Actions</th>
                </tr>
              </thead>
              <tbody>
                {categories.length === 0 ? (
                  <tr>
                    <td colSpan="4" style={{textAlign: 'center', padding: '40px', color: '#999'}}>
                      No categories found. Click "+ Add Category" to create one.
                    </td>
                  </tr>
                ) : (
                  categories.map(category => (
                    <tr key={category.categoryId}>
                      <td>{category.categoryId}</td>
                      <td><strong>{category.name}</strong></td>
                      <td>
                        <span className="badge badge-primary">
                          {category.subCategoriesCount} sub-categories
                        </span>
                      </td>
                      <td>
                        <button 
                          onClick={() => handleDelete(category.categoryId)}
                          className="delete-btn"
                        >
                          Delete
                        </button>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}

export default Categories;
