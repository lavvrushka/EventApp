import React, { useState } from 'react';
import './Home.css';
import { filterEvents } from '../services/api';
import EventSearch from '../components/EventSearch';

const Home = () => {
    const currentDate = new Date();
    const formattedDate = currentDate.toLocaleDateString(undefined, {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
    });

    const [filters, setFilters] = useState({
        address: '',
        city: '',
        state: '',
        country: '',
        category: '',
    });
    const [searchTerm, setSearchTerm] = useState('');
    const [events, setEvents] = useState([]);
    const [error, setError] = useState(null);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFilters({ ...filters, [name]: value });
    };

    const handleFilterSubmit = async (e) => {
        e.preventDefault();
        try {
            const filteredEvents = await filterEvents({ ...filters, search: searchTerm });
            const formattedEvents = filteredEvents.map(event => ({
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
            setError(null);
        } catch (err) {
            setError('Error fetching filtered events. Please try again.');
            console.error(err);
        }
    };

    const handleResetFilters = () => {
        setFilters({
            address: '',
            city: '',
            state: '',
            country: '',
            category: '',
        });
        setSearchTerm('');
        setEvents([]);
        setError(null);
    };

    return (
        <div className="home">
            <h2>Welcome to the Event Platform</h2>
            <p>Stay up to date with the latest events around you.</p>
            <p>From concerts to workshops – we have a wide variety of events for you.</p>

            <div className="date-time-info">
                <p><strong>Current Date:</strong> {formattedDate}</p>
            </div>
            <EventSearch />

            <form className="filter-form" onSubmit={handleFilterSubmit}>
                <h3>Filter Events</h3>
                <div>
                    <label>Address</label>
                    <input
                        type="text"
                        name="address"
                        value={filters.address}
                        onChange={handleInputChange}
                    />
                </div>
                <div>
                    <label>City</label>
                    <input
                        type="text"
                        name="city"
                        value={filters.city}
                        onChange={handleInputChange}
                    />
                </div>
                <div>
                    <label>State</label>
                    <input
                        type="text"
                        name="state"
                        value={filters.state}
                        onChange={handleInputChange}
                    />
                </div>
                <div>
                    <label>Country</label>
                    <input
                        type="text"
                        name="country"
                        value={filters.country}
                        onChange={handleInputChange}
                    />
                </div>
                <div>
                    <label>Category</label>
                    <input
                        type="text"
                        name="category"
                        value={filters.category}
                        onChange={handleInputChange}
                    />
                </div>
                <button type="submit">Apply Filters</button>
                <button type="button" onClick={handleResetFilters}>Reset Filters</button>
            </form>

            <div className="filtered-events">
                <h3>Filtered Events</h3>
                {error && <p className="error">{error}</p>}
                {events.length > 0 ? (
                    <ul>
                        {events.map((event) => (
                            <li key={event.id} className="event-card">
                                <h4>{event.title}</h4>
                                <p><strong>Date:</strong> {event.formattedDate}</p>
                                <p><strong>Description:</strong> {event.description}</p>
                                <p><strong>Location:</strong> {event.location}</p>
                            </li>
                        ))}
                    </ul>
                ) : (
                    <p>No events found with the selected filters.</p>
                )}
            </div>
        </div>
    );
};

export default Home;
