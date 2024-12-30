import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './pages/Home';
import EventListPage from './pages/EventListPage';
import EventDetailsPage from './pages/EventDetailsPage';
import AuthPage from './pages/AuthPage'; 
import EventManagementPage from './pages/EventManagementPage'; 
import UserEventsPage from './pages/UserEventsPage';

const App = () => {
  const [user, setUser] = useState(null); 

  useEffect(() => {
    const storedUser = localStorage.getItem('accessToken');
    if (storedUser) {
      setUser(true); 
    }
  }, []);

  const handleLogin = (userData) => {
    setUser(userData);
    localStorage.setItem('accessToken', userData.accessToken);
  };

  const handleLogout = () => {
    setUser(null);
    localStorage.removeItem('accessToken');
  };

  return (
    <Router>
      <Layout user={user} onLogout={handleLogout}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/events" element={<EventListPage />} />
          <Route path="/events/:eventId" element={<EventDetailsPage />} />
          <Route
            path="/auth"
            element={<AuthPage onLogin={handleLogin} />}
          />
          <Route
            path="/admin/events"
            element={user ? <EventManagementPage /> : <AuthPage onLogin={handleLogin} />}
          />
          <Route
            path="/user/events"
            element={user ? <UserEventsPage /> : <AuthPage onLogin={handleLogin} />}
          />
        </Routes>
      </Layout>
    </Router>
  );
};

export default App;
