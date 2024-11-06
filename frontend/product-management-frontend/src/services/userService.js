// src/services/userService.js
import api from './api';

// Register a new user
export const registerUser = (userData) => api.post('/Login/RegisterUser', userData);

// Login a user
export const loginUser = (loginData) => api.post('/Login/LoginUser', loginData);

// Get all users (if required)
export const getAllUsers = () => api.get('/users'); // Replace '/users' with the actual endpoint
