// src/services/orderItemService.js
import api from './api';

export const addOrderItem = (orderItemData) => api.post('/OrderItem/AddOrderItem', orderItemData);

export const getAllOrderItems = () => api.get('/OrderItem/GetAllOrderItems');

export const getOrderItemById = (id) => api.get(`/OrderItem/${id}`);

export const updateOrderItem = (id, orderItemData) => api.put('/OrderItem/UpdateOrderItem', { ...orderItemData, orderItemId: id });

export const deleteOrderItem = (id) => api.delete(`/OrderItem/DeleteOrderItem/${id}`);
