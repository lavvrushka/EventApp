import React, { useState } from 'react';
import { getEventsByTitle } from '../services/api';
import './SearchEvents.css';

const EventSearch = () => {
    const [query, setQuery] = useState('');
    const [events, setEvents] = useState([]);
    const [error, setError] = useState(null);

    const handleSearch = async () => {
        try {
            setError(null);
            let result = [];

            if (query.trim()) {
                result = await getEventsByTitle(query);
            }

            if (result && result.id) {
                result = [result];
            }

            if (Array.isArray(result)) {
                const formattedEvents = result.map(event => ({
                    ...event,
                    formattedDate: new Date(event.dateTime).toLocaleDateString(undefined, {
                        weekday: 'long',
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                    }),
                    location: event.location ? `${event.location.city}, ${event.location.country}` : 'Unknown location',
                    usersCount: event.users ? event.users.length : 0,
                }));

                setEvents(formattedEvents);
            } else {
                throw new Error('No events array found.');
            }
        } catch (err) {
            console.error('Error while fetching events:', err);
            setError(err.message || 'Failed to fetch events. Please try again.');
        }
    };

    return (
        <div className="event-search">
            <h3>Search for Events</h3>
            <div className="search-controls">
                <input
                    type="text"
                    value={query}
                    onChange={(e) => setQuery(e.target.value)}
                    placeholder="Enter event title"
                />
                <button onClick={handleSearch}>Search</button>
            </div>

            {error && <p className="error">{error}</p>}

            <div className="event-list">
                {events.length > 0 ? (
                    <ul>
                        {events.map((event) => (
                            <li key={event.id}>
                                <h4>{event.title}</h4>
                                <p><strong>Date:</strong> {event.formattedDate}</p>
                                <p><strong>Description:</strong> {event.description}</p>
                                <p><strong>Location:</strong> {event.location}</p>
                                <p><strong>Users Registered:</strong> {event.usersCount}</p>
                            </li>
                        ))}
                    </ul>
                ) : (
                    <p>No events found.</p>
                )}
            </div>
        </div>
    );
};

export default EventSearch;
