import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import Flights from './pages/Flights';
import Passengers from './pages/Passengers';
import Tickets from './pages/Tickets';
import ValidateTicket from './pages/ValidateTicket';
import Categories from './pages/Categories';
import SubCategories from './pages/SubCategories';
import ProductsCustoms from './pages/ProductsCustoms';
import BaggageTracking from './pages/BaggageTracking';
import BaggageDrop from './pages/BaggageDrop';
import BoardingPass from './pages/BoardingPass';
import './App.css';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/flights" element={<Flights />} />
        <Route path="/passengers" element={<Passengers />} />
        <Route path="/tickets" element={<Tickets />} />
        <Route path="/validate" element={<ValidateTicket />} />
        <Route path="/categories" element={<Categories />} />
        <Route path="/subcategories" element={<SubCategories />} />
        <Route path="/products-customs" element={<ProductsCustoms />} />
        <Route path="/baggage-tracking" element={<BaggageTracking />} />
        <Route path="/baggage-drop" element={<BaggageDrop />} />
        <Route path="/boarding-pass" element={<BoardingPass />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
