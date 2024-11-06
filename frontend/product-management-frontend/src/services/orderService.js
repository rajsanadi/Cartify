// src/services/orderService.js
import api from './api';

export const getAllOrders = () => api.get('/Order/GetAllOrders');

export const getOrderById = (id) => api.get(`/Order/${id}`);

export const addOrder = (orderData) => api.post('/Order/AddOrder', orderData);

export const updateOrder = (id, orderData) => api.put('/Order/UpdateOrder', { ...orderData, orderId: id });

export const deleteOrder = (id) => api.delete(`/Order/DeleteOrder/${id}`);

export const getProductsByOrderId = (orderId) => api.get(`/Order/GetProductsByOrderId/${orderId}`);
