// src/components/Orders/OrderList.js
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllOrders, deleteOrder } from '../../services/orderService';

const OrderList = () => {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    fetchOrders();
  }, []);

  const fetchOrders = async () => {
    try {
      const response = await getAllOrders();
      setOrders(response.data);
    } catch (error) {
      console.error(error);
      alert('Failed to fetch orders.');
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this order?')) {
      try {
        await deleteOrder(id);
        fetchOrders();
      } catch (error) {
        console.error(error);
        alert('Failed to delete order.');
      }
    }
  };

  return (
    <div>
      <h2>Orders</h2>
      <Link to="/orders/new">Add New Order</Link>
      <table>
        <thead>
          <tr>
            <th>Order Date</th>
            <th>Total Amount</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {orders.map((order) => (
            <tr key={order.orderId}>
              <td>{new Date(order.orderDate).toLocaleDateString()}</td>
              <td>{order.totalAmount}</td>
              <td>
                <Link to={`/orders/${order.orderId}`}>View</Link>
                <Link to={`/orders/edit/${order.orderId}`}>Edit</Link>
                <button onClick={() => handleDelete(order.orderId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default OrderList;
