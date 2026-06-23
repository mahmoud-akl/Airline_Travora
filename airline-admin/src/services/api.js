import axios from 'axios';

// Use environment variable for API URL (falls back to localhost for development)
const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5034/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add token to requests if available
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Auth
export const login = (username, password) =>
  api.post('/admin/auth/login', { username, password });

// Flights
export const getFlights = () => api.get('/admin/flights');
export const getFlight = (id) => api.get(`/admin/flights/${id}`);
export const createFlight = (data) => api.post('/admin/flights', data);
export const updateFlight = (id, data) => api.put(`/admin/flights/${id}`, data);
export const deleteFlight = (id) => api.delete(`/admin/flights/${id}`);
export const updateFlightStatus = (id, newStatus) => 
  api.patch(`/admin/flights/${id}/status`, { Status: newStatus });

// Passengers
export const getPassengers = () => api.get('/admin/passengers');
export const getPassenger = (id) => api.get(`/admin/passengers/${id}`);
export const createPassenger = (data) => api.post('/admin/passengers', data);
export const updatePassenger = (id, data) => api.put(`/admin/passengers/${id}`, data);
export const deletePassenger = (id) => api.delete(`/admin/passengers/${id}`);

// Tickets
export const getTickets = () => api.get('/admin/tickets');
export const getTicket = (id) => api.get(`/admin/tickets/${id}`);
export const createTicket = (data) => api.post('/admin/tickets', data);
export const updateTicket = (id, data) => api.put(`/admin/tickets/${id}`, data);
export const deleteTicket = (id) => api.delete(`/admin/tickets/${id}`);
export const generateBoardingPass = (ticketNumber) =>
  api.post(`/admin/tickets/${ticketNumber}/generate-boarding-pass`);
export const generateBaggageTags = (ticketNumber, count) =>
  api.post(`/admin/tickets/${ticketNumber}/generate-baggage-tags?count=${count}`);

// Public APIs
export const validateTicket = (data) => api.post('/airline/validate-ticket', data);

// Baggage Tags
export const getBaggageTags = () => api.get('/admin/baggage-tags');
export const getBaggageTagsByTicket = (ticketNumber) => 
  api.get(`/admin/baggage-tags/ticket/${ticketNumber}`);

// Customs - Public API
export const lookupProduct = (productName) => 
  api.get(`/customs/lookup?productName=${encodeURIComponent(productName)}`);

// Customs - Categories
export const getCategories = () => api.get('/admin/customs/categories');
export const createCategory = (data) => api.post('/admin/customs/categories', data);
export const deleteCategory = (id) => api.delete(`/admin/customs/categories/${id}`);

// Customs - SubCategories
export const getSubCategories = () => api.get('/admin/customs/subcategories');
export const createSubCategory = (data) => api.post('/admin/customs/subcategories', data);
export const deleteSubCategory = (id) => api.delete(`/admin/customs/subcategories/${id}`);

// Customs - Products
export const getProducts = () => api.get('/admin/customs/products');
export const searchProducts = (query) => api.get(`/admin/customs/products/search?q=${encodeURIComponent(query)}`);
export const createProduct = (data) => api.post('/admin/customs/products', data);
export const updateProduct = (id, data) => api.put(`/admin/customs/products/${id}`, data);
export const deleteProduct = (id) => api.delete(`/admin/customs/products/${id}`);

// Baggage Location Tracking
export const getBaggageByTicket = (ticketNumber) =>
  api.get(`/airline/baggage/by-ticket/${ticketNumber}`);
export const updateBaggageLocation = (tagNumber, data) =>
  api.patch(`/airline/baggage/${tagNumber}/location`, data);
export const getBaggageLastLocation = (tagNumber) =>
  api.get(`/airline/baggage/${tagNumber}/last-location`);
export const updateAllBaggageByTicket = (ticketNumber, data) =>
  api.patch(`/airline/baggage/by-ticket/${ticketNumber}/location`, data);
export const updateBaggageWeight = (tagNumber, weightKg) =>
  api.patch(`/airline/baggage/${tagNumber}/weight`, { weightKg });
export const deleteBaggageTag = (tagNumber) => api.delete(`/airline/baggage/${tagNumber}`);
export const deleteAllBaggageByTicket = (ticketNumber) => api.delete(`/airline/baggage/by-ticket/${ticketNumber}`);

// Boarding Pass
export const issueBoardingPass = (ticketNumber) =>
  api.post('/airline/issue-boarding-pass', { ticketNumber });

// Ticket Baggage Allowance
export const getTicketBaggageAllowance = (ticketNumber) =>
  api.get(`/airline/tickets/${ticketNumber}/baggage-allowance`);

export default api;
