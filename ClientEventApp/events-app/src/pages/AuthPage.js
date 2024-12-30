import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginUser, registerUser } from '../services/api';
import './AuthPage.css'; 
import image from '../images/image.png'; 

const AuthPage = ({ onLogin }) => {
  const [isLogin, setIsLogin] = useState(true);
  const [formData, setFormData] = useState({
    firstname: '',
    lastname: '',
    email: '',
    password: '',
    confirmPassword: '',
    birthDate: '',
  });
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const validateForm = () => {
    if (!isLogin) {
      if (!/^[a-zA-Z]+$/.test(formData.firstname)) {
        return 'First name should contain only letters';
      }
      if (!/^[a-zA-Z]+$/.test(formData.lastname)) {
        return 'Last name should contain only letters';
      }
      const birthDate = new Date(formData.birthDate);
      const age = new Date().getFullYear() - birthDate.getFullYear();
      if (age < 18) {
        return 'You must be at least 18 years old';
      }
    }

    if (!/^\S+@\S+\.\S+$/.test(formData.email)) {
      return 'Please enter a valid email address';
    }

    if (formData.password.length < 6) {
      return 'Password must be at least 6 characters long';
    }

    if (!isLogin && formData.password !== formData.confirmPassword) {
      return 'Passwords do not match';
    }

    return null;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);

    const validationError = validateForm();
    if (validationError) {
      setError(validationError);
      return;
    }

    try {
      const requestBody = {
        firstname: formData.firstname,
        lastname: formData.lastname,
        password: formData.password,
        email: formData.email,
        birthDate: isLogin ? undefined : new Date(formData.birthDate).toISOString(),
      };

      if (isLogin) {
        const response = await loginUser({
          email: formData.email,
          password: formData.password,
        });

        if (response?.data?.accessToken) {
          localStorage.setItem('accessToken', response.data.accessToken);
        }

        onLogin(response);
        navigate('/');
      } else {
        await registerUser(requestBody);
        alert('Registration successful! Please log in.');
        setIsLogin(true);
      }
    } catch (err) {
      setError(err.response?.data?.message || 'An error occurred');
    }
  };

  const handleToggle = () => {
    setIsLogin((prev) => {
      const newIsLogin = !prev;
      if (newIsLogin) {
        setFormData((prevData) => ({ ...prevData, birthDate: '' }));
      }
      return newIsLogin;
    });
  };

  return (
    <div className="authContainer">
      <img src={image} alt="Auth" className="authImage" />
      <form onSubmit={handleSubmit} className="authForm">
        <h2>{isLogin ? 'Log In' : 'Register'}</h2>

        {!isLogin && (
          <>
            <div className="inputGroup">
              <label>First Name:</label>
              <input
                type="text"
                name="firstname"
                value={formData.firstname}
                onChange={handleChange}
                required
              />
            </div>
            <div className="inputGroup">
              <label>Last Name:</label>
              <input
                type="text"
                name="lastname"
                value={formData.lastname}
                onChange={handleChange}
                required
              />
            </div>
            <div className="inputGroup">
              <label>Birth Date:</label>
              <input
                type="date"
                name="birthDate"
                value={formData.birthDate}
                onChange={handleChange}
                required
              />
            </div>
          </>
        )}

        <div className="inputGroup">
          <label>Email:</label>
          <input
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>

        <div className="inputGroup">
          <label>Password:</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>

        {!isLogin && (
          <div className="inputGroup">
            <label>Confirm Password:</label>
            <input
              type="password"
              name="confirmPassword"
              value={formData.confirmPassword}
              onChange={handleChange}
              required
            />
          </div>
        )}

        {error && <p className="errorMessage">{error}</p>}
        <button type="submit">{isLogin ? 'Log In' : 'Register'}</button>
        <button onClick={handleToggle} className="toggleButton">
          {isLogin ? 'Register' : 'Log In'}
        </button>
      </form>
    </div>
  );
};

export default AuthPage;
