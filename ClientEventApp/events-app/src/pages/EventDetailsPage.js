import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getEventById, registerUserToEvent, deleteUserFromEvent } from '../services/api';
import './EventDetailsPage.css';
import { useNavigate } from 'react-router-dom'; 

const EventDetailsPage = () => {
  const { eventId } = useParams();
  const [event, setEvent] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [participants, setParticipants] = useState([]);
  const [registrationStatus, setRegistrationStatus] = useState('');
  const [isUserRegistered, setIsUserRegistered] = useState(false);
  const navigate = useNavigate(); 
  const [currentPage, setCurrentPage] = useState(1);
  const [participantsPerPage] = useState(5); 
  
  const userId = JSON.parse(localStorage.getItem('user')).id; 
  const token = localStorage.getItem('accessToken');

  useEffect(() => {
    const fetchEvent = async () => {
      try {
        const data = await getEventById(eventId);
        setEvent(data);
        setParticipants(data.users || []);
        const userRegistered = data.users.some((participant) => participant.id === userId);
        setIsUserRegistered(userRegistered);
      } catch (err) {
        setError('Failed to load event details');
      } finally {
        setLoading(false);
      }
    };

    fetchEvent();
  }, [eventId, userId]);

  const handleRegister = async () => {
    if (!token) {
      navigate('/auth'); 
      return;
    }

    try {
      await registerUserToEvent({ idEvent: event.id }, token);
      setRegistrationStatus('Successfully registered!');
      setIsUserRegistered(true); 
      setParticipants((prev) => [...prev, { id: userId }]); 
    } catch (err) {
      setError('Failed to register for the event');
    }
  };

  const handleUnregister = async () => {
    if (!token) {
      navigate('/auth');
      return;
    }

    try {
      await deleteUserFromEvent({ idEvent: event.id, idUser: userId });
      setRegistrationStatus('Successfully unregistered!');
      setIsUserRegistered(false); 
      setParticipants((prev) => prev.filter((participant) => participant.id !== userId)); 
    } catch (err) {
      setError('Failed to unregister from the event');
    }
  };

  const renderActionButton = () => {
    if( event.maxUsers === participants.length)
      return(<p>There are no more places</p>)

    
    if (!token) {
      return (
        <button className="register-button" onClick={handleRegister}>
          Register for this event
        </button>
      );
    }

    if (isUserRegistered) {
      return (
        <button className="unregister-button" onClick={handleUnregister}>
          Unregister from this event
        </button>
      );
    }



    return (
      <button className="register-button" onClick={handleRegister}>
        Register for this event
      </button>
    );
  };

  const indexOfLastParticipant = currentPage * participantsPerPage;
  const indexOfFirstParticipant = indexOfLastParticipant - participantsPerPage;
  const currentParticipants = participants.slice(indexOfFirstParticipant, indexOfLastParticipant);
  const paginate = (pageNumber) => setCurrentPage(pageNumber);
  const totalPages = Math.ceil(participants.length / participantsPerPage);

  if (loading) return <div className="loader">Loading...</div>;
  if (error) return <div className="error">{error}</div>;
  if (!event) return <div className="error">Event not found</div>;

  return (
    <div className="event-details-page">
      <div className="event-details-content animate-fade-in">
        <div className="event-details-card glass-card">
          <h1 className="event-title">{event.title}</h1>
          
          {event.imageData && (
            <img
              src={`data:${event.imageType};base64,${event.imageData}`}
              alt={event.title}
              className="event-images"
            />
          )}
          
          <p className="event-description">{event.description}</p>
          <div className="event-meta">
            <p><strong>Category:</strong> {event.category}</p>
            <p><strong>Location:</strong> {event.location.city}, {event.location.country}</p>
            <p><strong>Date:</strong> {new Date(event.dateTime).toLocaleString()}</p>
            <p><strong>Max Users:</strong> {event.maxUsers}</p>
          </div>

          {renderActionButton()}

          {registrationStatus && <p className="registration-status">{registrationStatus}</p>}
        </div>
      </div>

      <div className="participants-section animate-slide-in">
        <h2 className="participants-title">Participants</h2>
        <ul className="participants-list">
          {currentParticipants.length > 0 ? (
            currentParticipants.map((participant, index) => (
              <li key={index} className="participant-card glass-card">
                <p className="participant-name">{participant.firstName} {participant.lastName}</p>
                <p className="participant-email">{participant.email}</p>
              </li>
            ))
          ) : (
            <p className="no-participants">No participants yet</p>
          )}
        </ul>
      </div>
    </div>
  );
};

export default EventDetailsPage;
