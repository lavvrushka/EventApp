import React from 'react';
import './Footer.css';

const Footer = () => {
  return (
    <footer className="footer">
      <p>
        © {new Date().getFullYear()} Welcome to our <a href="/">Events Platform</a>. All rights reserved.
      </p>
    </footer>
  );
};

export default Footer;