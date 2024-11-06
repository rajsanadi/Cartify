// src/components/Orders/OrderForm.js
import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { addOrder, getOrderById, updateOrder } from '../../services/orderService';
import { getAllUsers } from '../../services/userService'; // Assume you have a user service
import OrderItemForm from './OrderItemForm'; // Component to handle order items

const OrderForm = () => {
  const { id } = useParams();
  const isEdit = Boolean(id);
  const navigate = useNavigate();

  const [order, setOrder] = useState({
    orderDate: '',
    totalAmount: '',
    userId: '',
    orderItems: [],
  });

  const [users, setUsers] = useState([]);

  useEffect(() => {
    fetchUsers();
    if (isEdit) {
      fetchOrder();
    }
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await getAllUsers();
      setUsers(response.data);
    } catch (error) {
      console.error(error);
      alert('Failed to fetch users.');
    }
  };

  const fetchOrder = async () => {
    try {
      const response = await getOrderById(id);
      setOrder({
        orderDate: response.data.orderDate,
        totalAmount: response.data.totalAmount,
        userId: response.data.userId,
        orderItems: response.data.orderItems,
      });
    } catch (error) {
      console.error(error);
      alert('Failed to fetch order details.');
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setOrder((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleOrderItemsChange = (orderItems) => {
    setOrder((prev) => ({
      ...prev,
      orderItems,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (isEdit) {
        await updateOrder(id, order);
        alert('Order updated successfully.');
      } else {
        await addOrder(order);
        alert('Order added successfully.');
      }
      navigate('/orders');
    } catch (error) {
      console.error(error);
      alert('Failed to save order.');
    }
  };

  return (
    <div>
      <h2>{isEdit ? 'Edit Order' : 'Add New Order'}</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Order Date:</label>
          <input
            type="date"
            name="orderDate"
            value={order.orderDate.split('T')[0]}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>Total Amount:</label>
          <input
            type="number"
            name="totalAmount"
            value={order.totalAmount}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>User:</label>
          <select name="userId" value={order.userId} onChange={handleChange} required>
            <option value="">Select User</option>
            {users.map((user) => (
              <option key={user.id} value={user.id}>
                {user.username}
              </option>
            ))}
          </select>
        </div>
        <OrderItemForm orderItems={order.orderItems} onChange={handleOrderItemsChange} />
        <button type="submit">{isEdit ? 'Update' : 'Add'} Order</button>
      </form>
    </div>
  );
};

export default OrderForm;
