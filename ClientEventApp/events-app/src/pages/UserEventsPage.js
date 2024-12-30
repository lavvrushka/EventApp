import React, { useState, useEffect } from 'react';
import { getUserEvents } from '../services/api';
import './UserEventsPage.css'; 

const UserEventsPage = () => {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchUserEvents = async () => {
      const user = JSON.parse(localStorage.getItem('user')); 

      if (!user.id) {
        setError('Unable to find user ID.');
        console.log('Events fetched:');
        setLoading(false);
        return;
      }

      try {
        const userEvents = await getUserEvents(user.id); 
        console.log('Events fetched:', userEvents); 
        
        if (Array.isArray(userEvents)) {
          setEvents(userEvents);
        } else if (userEvents.items && Array.isArray(userEvents.items)) {
          setEvents(userEvents.items); 
        } else {
          throw new Error('Fetched data does not have the expected structure');
        }
        setLoading(false);
      } catch (err) {
        setError('Failed to load events');
        setLoading(false);
        console.error(err);
      }
    };

    fetchUserEvents();
  }, []); 

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div className="user-events-container">
      <h2>Your Events</h2>

      <div className="page-description">
        <p>
          Welcome to your events page! Here you can view all the events you are currently participating in.
          The list includes details such as event title, date/time, location, and the maximum number of participants.
          If you are not participating in any events yet, you will see a message indicating that.
        </p>
      </div>

      {events.length === 0 ? (
        <p>You are not participating in any events yet.</p>
      ) : (
        <ul className="event-list">
          {events.map((event) => (
            <li key={event.id} className="event-item">
              <h3>{event.title}</h3>
              <p><strong>Date/Time:</strong> {new Date(event.dateTime).toLocaleString()}</p>
              <p><strong>Description:</strong> {event.description}</p>
              <p><strong>Location:</strong> {event.location.city}, {event.location.country}</p>
              <p><strong>Max Participants:</strong> {event.maxUsers}</p>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default UserEventsPage;
