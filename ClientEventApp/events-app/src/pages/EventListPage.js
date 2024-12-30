import React, { useState, useEffect } from 'react';
import { getAllEvents } from '../services/api';
import { useNavigate } from 'react-router-dom';
import './EventListPage.css';

const EventListPage = () => {
  const [events, setEvents] = useState([]);
  const [pagination, setPagination] = useState({
    currentPage: 1,
    totalPages: 1,
    pageSize: 3,
    totalCount: 0,
    hasPrevious: false,
    hasNext: false,
  });

  const navigate = useNavigate();

  const fetchEvents = async (pageIndex = 1, pageSize = 2) => {
    try {
      const response = await getAllEvents(pageIndex, pageSize);
      setEvents(response.items || []);
      setPagination({
        currentPage: response.currentPage,
        totalPages: response.totalPages,
        pageSize: response.pageSize,
        totalCount: response.totalCount,
        hasPrevious: response.hasPrevious,
        hasNext: response.hasNext,
      });
    } catch (error) {
      console.error('Error fetching events:', error);
    }
  };

  useEffect(() => {
    fetchEvents(pagination.currentPage, pagination.pageSize);
  }, [pagination.currentPage]);

  const handlePageChange = (pageIndex) => {
    setPagination((prevState) => ({
      ...prevState,
      currentPage: pageIndex,
    }));
  };

  const handleEventClick = (id) => {
    navigate(`/events/${id}`); 
  };

  const generatePageNumbers = () => {
    const pageNumbers = [];
    for (let i = 1; i <= pagination.totalPages; i++) {
      pageNumbers.push(i);
    }
    return pageNumbers;
  };

  return (
    <div className="event-list-page">
      <h1 className="page-title">Our Events</h1>

      {events && events.length === 0 ? (
        <p className="no-events">No events available.</p>
      ) : (
        <div className="event-cards">
          {events.map((event) => (
            <div
              key={event.id}
              className="event-card"
              onClick={() => handleEventClick(event.id)} 
            >
              <img
                    src={`data:${event.imageType};base64,${event.imageData}`}
                    alt={event.title}
                    className="event-image"
                />
              <div className="event-details">
                <h3 className="event-title">{event.title}</h3>
                <p className="event-description">{event.description}</p>
                <div className="event-info">
                  <p><strong>Category:</strong> {event.category}</p>
                  <p><strong>Location:</strong> {event.location.city}, {event.location.country}</p>
                  <p><strong>Date:</strong> {new Date(event.dateTime).toLocaleString()}</p>
                  <p><strong>Max Users:</strong> {event.maxUsers}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      <div className="pagination-controls">
        <div className="page-numbers">
          {generatePageNumbers().map((pageNumber) => (
            <button
              key={pageNumber}
              onClick={() => handlePageChange(pageNumber)}
              className={`page-button ${pagination.currentPage === pageNumber ? 'active' : ''}`}
            >
              {pageNumber}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
};

export default EventListPage;
