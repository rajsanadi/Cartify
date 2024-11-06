// src/services/productService.js
import api from './api';

export const getAllProducts = () => api.get('/Product/GetAllProducts');

export const getProductById = (id) => api.get(`/Product/${id}`);

export const getProductByName = (name) => api.get(`/Product/GetProductByName/${name}`);

export const addProduct = (productData) => api.post('/Product/AddProducts', productData);

export const updateProduct = (id, productData) => api.put(`/Product/UpdateProducts`, { ...productData, productId: id });

export const deleteProduct = (id) => api.delete(`/Product/DeleteProducts/${id}`);
