import React, { useState } from 'react';
import { registerUser } from '../../services/userService'; // Use userService for consistency
import { useNavigate } from 'react-router-dom';

const Register = () => {
  const [userName, setUserName] = useState('');
  const [userEmail, setUserEmail] = useState('');
  const [userPassword, setUserPassword] = useState('');
  const [userPhoneNo, setUserPhoneNo] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Basic client-side validation
    if (userName.trim() === '' || userEmail.trim() === '' || userPassword.trim() === '' || userPhoneNo.trim() === '') {
      alert('All fields are required.');
      return;
    }

    if (userPassword.length < 6) {
      alert('Password must be at least 6 characters long.');
      return;
    }

    try {
      // Create FormData to handle multipart/form-data
      const formData = new FormData();
      formData.append('UserName', userName);
      formData.append('UserEmail', userEmail);
      formData.append('UserPassword', userPassword);
      formData.append('UserPhoneNo', userPhoneNo);

      // Call the registerUser function
      const response = await registerUser(formData);
      console.log('Registration Response:', response.data);
      alert('Registration successful. Please log in.');
      navigate('/login');
    } catch (error) {
      if (error.response) {
        console.error('Registration Error:', error.response);
        alert(`Registration failed: ${error.response.data.message || 'Please try again.'}`);
      } else if (error.request) {
        console.error('No response received:', error.request);
        alert('Registration failed: No response from server.');
      } else {
        console.error('Error:', error.message);
        alert(`Registration failed: ${error.message}`);
      }
    }
  };

  return (
    <div style={styles.container}>
      <h2>Register</h2>
      <form onSubmit={handleSubmit} style={styles.form}>
        <div style={styles.inputGroup}>
          <label htmlFor="userName" style={styles.label}>Username:</label>
          <input
            type="text"
            id="userName"
            name="userName"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            required
            autoComplete="username"
            style={styles.input}
          />
        </div>
        <div style={styles.inputGroup}>
          <label htmlFor="userEmail" style={styles.label}>Email:</label>
          <input
            type="email"
            id="userEmail"
            name="userEmail"
            value={userEmail}
            onChange={(e) => setUserEmail(e.target.value)}
            required
            autoComplete="email"
            style={styles.input}
          />
        </div>
        <div style={styles.inputGroup}>
          <label htmlFor="userPassword" style={styles.label}>Password:</label>
          <input
            type="password"
            id="userPassword"
            name="userPassword"
            value={userPassword}
            onChange={(e) => setUserPassword(e.target.value)}
            required
            autoComplete="new-password"
            style={styles.input}
          />
        </div>
        <div style={styles.inputGroup}>
          <label htmlFor="userPhoneNo" style={styles.label}>Phone Number:</label>
          <input
            type="text"
            id="userPhoneNo"
            name="userPhoneNo"
            value={userPhoneNo}
            onChange={(e) => setUserPhoneNo(e.target.value)}
            required
            style={styles.input}
          />
        </div>
        <button type="submit" style={styles.button}>Register</button>
      </form>
    </div>
  );
};

// Simple inline styles for better presentation
const styles = {
  container: {
    maxWidth: '400px',
    margin: '50px auto',
    padding: '20px',
    border: '1px solid #ccc',
    borderRadius: '5px',
  },
  form: {
    display: 'flex',
    flexDirection: 'column',
  },
  inputGroup: {
    marginBottom: '15px',
  },
  label: {
    marginBottom: '5px',
    display: 'block',
    fontWeight: 'bold',
  },
  input: {
    padding: '8px',
    width: '100%',
    boxSizing: 'border-box',
  },
  button: {
    padding: '10px',
    backgroundColor: '#28a745',
    color: '#fff',
    border: 'none',
    borderRadius: '3px',
    cursor: 'pointer',
  },
};

export default Register;
