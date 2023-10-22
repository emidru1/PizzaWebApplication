import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar/NavBar';
import Pizzas from './components/Pizzas/Pizzas';
import MyOrders from './components/MyOrders/MyOrders';
import OrderPizza from './components/OrderPizza/OrderPizza';

function App() {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/" element={<Pizzas />} />
        <Route path="/order" element={<OrderPizza />} />
        <Route path="/orders" element={<MyOrders />} />
      </Routes>
    </Router>
  );
}

export default App;
