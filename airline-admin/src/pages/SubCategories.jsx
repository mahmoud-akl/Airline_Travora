import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getSubCategories, getCategories, createSubCategory, deleteSubCategory } from '../services/api';
import './Dashboard.css';
import './AdminPage.css';

function SubCategories({ onLogout }) {
  const [subCategories, setSubCategories] = useState([]);
  const [filteredSubCategories, setFilteredSubCategories] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [filterCategory, setFilterCategory] = useState('');
  const [formData, setFormData] = useState({ categoryId: '', name: '' });
  const navigate = useNavigate();

  useEffect(() => {
    loadData();
  }, []);

  useEffect(() => {
    filterData();
  }, [filterCategory, subCategories]);

  const loadData = async () => {
    try {
      const [subCatsRes, catsRes] = await Promise.all([
        getSubCategories(),
        getCategories()
      ]);
      setSubCategories(subCatsRes.data);
      setCategories(catsRes.data);
      setLoading(false);
    } catch (error) {
      console.error('Error loading data:', error);
      setLoading(false);
    }
  };

  const filterData = () => {
    if (!filterCategory) {
      setFilteredSubCategories(subCategories);
    } else {
      setFilteredSubCategories(
        subCategories.filter(sc => sc.categoryId === parseInt(filterCategory))
      );
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createSubCategory({
        categoryId: parseInt(formData.categoryId),
        name: formData.name
      });
      setShowForm(false);
      setFormData({ categoryId: '', name: '' });
      loadData();
      alert('Sub Category created successfully!');
    } catch (error) {
      alert('Failed to create sub category');
    }
  };

  const handleDelete = async (id) => {
    if (!confirm('Are you sure you want to delete this sub category?')) return;
    try {
      await deleteSubCategory(id);
      loadData();
      alert('Sub Category deleted successfully!');
    } catch (error) {
      alert('Failed to delete sub category');
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
          <h2>Sub Categories</h2>
          <div className="header-actions">
            <button onClick={() => navigate('/dashboard')} className="btn btn-secondary">
              ← Back to Dashboard
            </button>
            <button onClick={() => setShowForm(!showForm)} className="btn btn-primary">
              {showForm ? 'Cancel' : '+ Add Sub Category'}
            </button>
          </div>
        </div>

        {/* Filter Section */}
        <div className="filter-section">
          <div className="form-row">
            <div className="form-group">
              <label>Filter by Category</label>
              <select
                value={filterCategory}
                onChange={(e) => setFilterCategory(e.target.value)}
              >
                <option value="">All Categories</option>
                {categories.map(cat => (
                  <option key={cat.categoryId} value={cat.categoryId}>
                    {cat.name}
                  </option>
                ))}
              </select>
            </div>
            {filterCategory && (
              <button 
                onClick={() => setFilterCategory('')}
                className="btn btn-secondary"
                style={{alignSelf: 'flex-end'}}
              >
                Clear Filter
              </button>
            )}
          </div>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} className="add-form">
            <div className="form-row">
              <div className="form-group">
                <label>Category</label>
                <select
                  value={formData.categoryId}
                  onChange={(e) => setFormData({...formData, categoryId: e.target.value})}
                  required
                >
                  <option value="">Select Category</option>
                  {categories.map(cat => (
                    <option key={cat.categoryId} value={cat.categoryId}>
                      {cat.name}
                    </option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label>Sub Category Name</label>
                <input
                  type="text"
                  value={formData.name}
                  onChange={(e) => setFormData({...formData, name: e.target.value})}
                  required
                />
              </div>
            </div>
            <button type="submit" className="submit-btn">Save Sub Category</button>
          </form>
        )}

        {loading ? (
          <div className="loading">Loading...</div>
        ) : (
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Name</th>
                  <th>Category</th>
                  <th>Products</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filteredSubCategories.length === 0 ? (
                  <tr>
                    <td colSpan="5" style={{textAlign: 'center', padding: '40px', color: '#999'}}>
                      {filterCategory ? 'No sub categories found for this category' : 'No sub categories found'}
                    </td>
                  </tr>
                ) : (
                  filteredSubCategories.map(subCat => (
                    <tr key={subCat.subCategoryId}>
                      <td>{subCat.subCategoryId}</td>
                      <td>{subCat.name}</td>
                      <td>
                        <span className="badge badge-info">{subCat.categoryName}</span>
                      </td>
                      <td>
                        <span className="badge badge-primary">
                          {subCat.productsCount} products
                        </span>
                      </td>
                      <td>
                        <button 
                          onClick={() => handleDelete(subCat.subCategoryId)}
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

export default SubCategories;
