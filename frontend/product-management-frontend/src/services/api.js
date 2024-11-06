// src/services/api.js
import axios from 'axios';

const API_URL = 'https://localhost:7021/api'; // Update if necessary

const api = axios.create({
  baseURL: API_URL,
});

// Add a request interceptor to include the JWT token in headers
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('tokens'); // Retrieve as string
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api;
