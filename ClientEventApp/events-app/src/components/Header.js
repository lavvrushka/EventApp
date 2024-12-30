import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';

import './Header.css';

const Header = ({ user, onLogout }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('accessToken');
    onLogout();
    navigate('/');
  };

  return (
    <header className="header">
      <h1>Events</h1>
      <nav>
        <ul>
          <li><Link to="/">Home</Link></li>
          <li><Link to="/events">Catalog</Link></li>
          {!user ? (
            <li><Link to="/auth">Login/Register</Link></li>
          ) : (
            <li>
              <button style={{ display: "block" , textAlign: "center",
    marginTop: "15p",
    background: "none",
    border: "none",
    color: "#3498db",
    cursor: "pointer",
    fontSize: "20px",

  }}onClick={handleLogout}>Logout</button>
            </li>
          )}
          {user && (
            <li><Link to="/admin/events">Manage Events</Link></li>
          )}
          <li><Link to="/user/events">My Events</Link></li>

        </ul>
      </nav>
    </header>
  );
};


export default Header;
