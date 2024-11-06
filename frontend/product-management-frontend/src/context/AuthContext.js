// src/context/AuthContext.js
import React, { createContext, useState, useEffect } from 'react';
import {jwtDecode} from 'jwt-decode'; // Default import
import api from '../services/api'; // Ensure this path is correct
import axios from 'axios';

// Create the AuthContext
export const AuthContext = createContext();

// Provide the context to your app
export const AuthProvider = ({ children }) => {
  const [authTokens, setAuthTokens] = useState(() =>
    localStorage.getItem('tokens') ? localStorage.getItem('tokens') : null // Store as string
  );
  const [user, setUser] = useState(() =>
    localStorage.getItem('tokens') ? jwtDecode(localStorage.getItem('tokens')) : null
  );

  const loginUser = (token) => { // Expecting a single token string
    setAuthTokens(token);
    setUser(jwtDecode(token));
    localStorage.setItem('tokens', token); // Store as string
  };

  const logoutUser = () => {
    setAuthTokens(null);
    setUser(null);
    localStorage.removeItem('tokens');
  };

  const contextData = {
    user,
    authTokens,
    loginUser,
    logoutUser,
    isAuthenticated: !!authTokens,
  };

  return <AuthContext.Provider value={contextData}>{children}</AuthContext.Provider>;
};
