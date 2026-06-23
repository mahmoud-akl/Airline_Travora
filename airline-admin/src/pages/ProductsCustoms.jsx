import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getProducts, getSubCategories, getCategories, createProduct, updateProduct, deleteProduct, searchProducts } from '../services/api';
import './Dashboard.css';
import './AdminPage.css';

function ProductsCustoms({ onLogout }) {
  const [products, setProducts] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [subCategories, setSubCategories] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingProduct, setEditingProduct] = useState(null);
  const [searchQuery, setSearchQuery] = useState('');
  const [isSearchMode, setIsSearchMode] = useState(false);
  const [filterCategory, setFilterCategory] = useState('');
  const [filterSubCategory, setFilterSubCategory] = useState('');
  const [formData, setFormData] = useState({
    subCategoryId: '',
    name: '',
    customsRate: ''
  });
  const navigate = useNavigate();

  useEffect(() => {
    loadData();
  }, []);

  useEffect(() => {
    if (!isSearchMode) {
      filterData();
    }
  }, [filterCategory, filterSubCategory, products, isSearchMode]);

  const loadData = async () => {
    try {
      const [productsRes, subCatsRes, catsRes] = await Promise.all([
        getProducts(),
        getSubCategories(),
        getCategories()
      ]);
      setProducts(productsRes.data);
      setSubCategories(subCatsRes.data);
      setCategories(catsRes.data);
      setLoading(false);
    } catch (error) {
      console.error('Error loading data:', error);
      setLoading(false);
    }
  };

  const filterData = () => {
    let filtered = products;

    if (filterCategory) {
      const categoryName = categories.find(c => c.categoryId === parseInt(filterCategory))?.name;
      filtered = filtered.filter(p => p.categoryName === categoryName);
    }

    if (filterSubCategory) {
      filtered = filtered.filter(p => p.subCategoryId === parseInt(filterSubCategory));
    }

    setFilteredProducts(filtered);
  };

  const handleSearch = async () => {
    if (!searchQuery.trim()) {
      return;
    }
    try {
      setIsSearchMode(true);
      const response = await searchProducts(searchQuery);
      setFilteredProducts(response.data);
    } catch (error) {
      alert('Search failed');
    }
  };

  const handleClearSearch = () => {
    setSearchQuery('');
    setIsSearchMode(false);
    setFilterCategory('');
    setFilterSubCategory('');
    loadData();
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const payload = {
        subCategoryId: parseInt(formData.subCategoryId),
        name: formData.name,
        customsRate: parseFloat(formData.customsRate)
      };

      if (editingProduct) {
        await updateProduct(editingProduct.productId, {
          name: formData.name,
          customsRate: parseFloat(formData.customsRate)
        });
      } else {
        await createProduct(payload);
      }

      setShowForm(false);
      setEditingProduct(null);
      setFormData({ subCategoryId: '', name: '', customsRate: '' });
      loadData();
      alert(editingProduct ? 'Product updated successfully!' : 'Product created successfully!');
    } catch (error) {
      alert('Failed to save product');
    }
  };

  const handleEdit = (product) => {
    setEditingProduct(product);
    setFormData({
      subCategoryId: product.subCategoryId,
      name: product.name,
      customsRate: product.customsRate
    });
    setShowForm(true);
  };

  const handleDelete = async (id) => {
    if (!confirm('Are you sure you want to delete this product?')) return;
    try {
      await deleteProduct(id);
      loadData();
      alert('Product deleted successfully!');
    } catch (error) {
      alert('Failed to delete product');
    }
  };

  const handleLogout = () => {
    onLogout();
    navigate('/login');
  };

  const getFilteredSubCategories = () => {
    if (!filterCategory) return subCategories;
    const categoryName = categories.find(c => c.categoryId === parseInt(filterCategory))?.name;
    return subCategories.filter(sc => sc.categoryName === categoryName);
  };

  return (
    <div className="admin-page">
      <div className="navbar">
        <h1>Customs Management System</h1>
        <button onClick={handleLogout} className="logout-btn">Logout</button>
      </div>

      <div className="dashboard-content">
        <div className="page-header">
          <h2>Products & Customs Rates</h2>
          <div className="header-actions">
            <button onClick={() => navigate('/dashboard')} className="btn btn-secondary">
              ← Back to Dashboard
            </button>
            <button onClick={() => {
              setShowForm(!showForm);
              setEditingProduct(null);
              setFormData({ subCategoryId: '', name: '', customsRate: '' });
            }} className="btn btn-primary">
              {showForm ? 'Cancel' : '+ Add Product'}
            </button>
          </div>
        </div>

        {/* Search & Filter Section */}
        <div className={isSearchMode ? "filter-section search-mode" : "filter-section"}>
          {/* Search Bar */}
          <div className="search-bar" style={{marginBottom: isSearchMode ? '0' : '20px'}}>
            <input
              type="text"
              placeholder="🔍 Search products by name..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              onKeyPress={(e) => e.key === 'Enter' && handleSearch()}
              className={isSearchMode ? "search-active" : ""}
            />
            <button 
              onClick={handleSearch} 
              className="btn btn-search"
              disabled={!searchQuery.trim()}
            >
              <span style={{fontSize: '1.1rem'}}>🔍</span> Search
            </button>
            {(searchQuery || isSearchMode) && (
              <button onClick={handleClearSearch} className="btn btn-clear">
                ✕ Clear
              </button>
            )}
          </div>

          {/* Filters - Hidden when searching */}
          {!isSearchMode && (
            <>
              <div style={{
                borderTop: '1px solid #e2e8f0',
                margin: '20px 0 15px 0'
              }}></div>
              <div className="form-row">
                <div className="form-group">
                  <label>📁 Filter by Category</label>
                  <select
                    value={filterCategory}
                    onChange={(e) => {
                      setFilterCategory(e.target.value);
                      setFilterSubCategory('');
                    }}
                  >
                    <option value="">All Categories</option>
                    {categories.map(cat => (
                      <option key={cat.categoryId} value={cat.categoryId}>
                        {cat.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="form-group">
                  <label>📂 Filter by Sub Category</label>
                  <select
                    value={filterSubCategory}
                    onChange={(e) => setFilterSubCategory(e.target.value)}
                    disabled={!filterCategory}
                  >
                    <option value="">All Sub Categories</option>
                    {getFilteredSubCategories().map(subCat => (
                      <option key={subCat.subCategoryId} value={subCat.subCategoryId}>
                        {subCat.name}
                      </option>
                    ))}
                  </select>
                </div>
                {(filterCategory || filterSubCategory) && (
                  <button 
                    onClick={() => {
                      setFilterCategory('');
                      setFilterSubCategory('');
                    }}
                    className="btn btn-secondary"
                    style={{alignSelf: 'flex-end'}}
                  >
                    Clear Filters
                  </button>
                )}
              </div>
            </>
          )}

          {/* Search Mode Indicator */}
          {isSearchMode && (
            <div style={{
              marginTop: '15px',
              padding: '10px 15px',
              background: '#fef3c7',
              borderRadius: '8px',
              color: '#92400e',
              fontSize: '0.9rem',
              fontWeight: '500'
            }}>
              🔍 Showing search results for "{searchQuery}"
            </div>
          )}
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} className="add-form">
            <div className="form-row">
              <div className="form-group">
                <label>Sub Category</label>
                <select
                  value={formData.subCategoryId}
                  onChange={(e) => setFormData({...formData, subCategoryId: e.target.value})}
                  required
                  disabled={editingProduct}
                >
                  <option value="">Select Sub Category</option>
                  {subCategories.map(subCat => (
                    <option key={subCat.subCategoryId} value={subCat.subCategoryId}>
                      {subCat.categoryName} - {subCat.name}
                    </option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label>Product Name</label>
                <input
                  type="text"
                  value={formData.name}
                  onChange={(e) => setFormData({...formData, name: e.target.value})}
                  required
                />
              </div>
              <div className="form-group">
                <label>Customs Rate (decimal, e.g., 0.05 for 5%)</label>
                <input
                  type="number"
                  step="0.0001"
                  value={formData.customsRate}
                  onChange={(e) => setFormData({...formData, customsRate: e.target.value})}
                  required
                />
              </div>
            </div>
            <button type="submit" className="submit-btn">
              {editingProduct ? 'Update Product' : 'Save Product'}
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
                  <th>ID</th>
                  <th>Name</th>
                  <th>Category</th>
                  <th>Sub Category</th>
                  <th>Customs Rate</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {filteredProducts.length === 0 ? (
                  <tr>
                    <td colSpan="6" style={{textAlign: 'center', padding: '40px', color: '#999'}}>
                      {isSearchMode ? `No products found matching "${searchQuery}"` : 
                       filterCategory || filterSubCategory ? 'No products found for selected filters' : 
                       'No products found'}
                    </td>
                  </tr>
                ) : (
                  filteredProducts.map(product => (
                    <tr key={product.productId}>
                      <td>{product.productId}</td>
                      <td><strong>{product.name}</strong></td>
                      <td>
                        <span className="badge badge-info">{product.categoryName}</span>
                      </td>
                      <td>
                        <span className="badge badge-secondary">{product.subCategoryName}</span>
                      </td>
                      <td>
                        <span className="badge badge-success">
                          {(product.customsRate * 100).toFixed(2)}%
                        </span>
                      </td>
                      <td>
                        <button 
                          onClick={() => handleEdit(product)}
                          className="edit-btn"
                        >
                          Edit
                        </button>
                        {' '}
                        <button 
                          onClick={() => handleDelete(product.productId)}
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

export default ProductsCustoms;
