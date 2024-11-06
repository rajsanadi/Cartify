// src/App.js 
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import ProductList from './components/Products/ProductList';
import ProductDetail from './components/Products/ProductDetail';
import ProductForm from './components/Products/ProductForm';
import OrderList from './components/Orders/OrderList';
import OrderDetail from './components/Orders/OrderDetail';
import OrderForm from './components/Orders/OrderForm';
import PrivateRoute from './components/PrivateRoute';
import Navbar from './components/Navbar';
import { AuthProvider } from './context/AuthContext'; // Import AuthProvider
import SomeComponent from './components/SomeComponent';

function App() {
  return (
    <AuthProvider> {/* Wrap the entire Router with AuthProvider */}
      <Router>
        <Navbar />
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          
          {/* Private Routes */}
          <Route element={<PrivateRoute />}>
            <Route path="/products" element={<ProductList />} />
            <Route path="/products/:id" element={<ProductDetail />} />
            <Route path="/products/new" element={<ProductForm />} />
            <Route path="/products/edit/:id" element={<ProductForm />} />
            
            <Route path="/orders" element={<OrderList />} />
            <Route path="/orders/:id" element={<OrderDetail />} />
            <Route path="/orders/new" element={<OrderForm />} />
            <Route path="/orders/edit/:id" element={<OrderForm />} />
          </Route>

          {/* Default Route */}
          <Route path="*" element={<Login />} />
        </Routes>
      </Router>
      <SomeComponent/>
    </AuthProvider>
  );
}

export default App;
