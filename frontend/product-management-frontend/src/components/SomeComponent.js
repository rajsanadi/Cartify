// src/components/SomeComponent.js
import React, { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';

const SomeComponent = () => {
  const { user, isAuthenticated } = useContext(AuthContext);

  return (
    <div>
      {isAuthenticated ? (
        <p>Welcome, {user.username}!</p>
      ) : (
        <p>Please log in.</p>
      )}
    </div>
  );
};

export default SomeComponent;
