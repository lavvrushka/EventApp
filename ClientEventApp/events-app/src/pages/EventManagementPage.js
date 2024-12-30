import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getAllEvents, createEvent, updateEvent, deleteEvent } from '../services/api';
import './EventManagementPage.css'; 

const EventManagementPage = () => {
  const navigate = useNavigate();
  const [events, setEvents] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [currentEvent, setCurrentEvent] = useState(null);
  const [formData, setFormData] = useState({
    id: '',
    title: '',
    description: '',
    dateTime: '',
    location: { address: '', city: '', state: '', country: '' },
    category: '',
    maxUsers: 0,
    imageId: '',
    imageData: '',
    imageType: '',
  });
  const [errors, setErrors] = useState({}); // State to store validation errors
  const [imagePreview, setImagePreview] = useState(null); 
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize] = useState(3);
  const [totalPages, setTotalPages] = useState(1);
  const [role, setRole] = useState(localStorage.getItem('role'));

  const loadEvents = async () => {
    try {
      const fetchedEvents = await getAllEvents(pageIndex, pageSize);
      if (Array.isArray(fetchedEvents.items)) {
        setEvents(fetchedEvents.items);
        setTotalPages(fetchedEvents.totalPages);
      } else {
        console.error('Fetched data does not contain an events array');
        setEvents([]);
      }
    } catch (error) {
      console.error('Error loading events:', error);
    }
  };

  useEffect(() => {
    const role = JSON.parse(localStorage.getItem('role'));
    setRole(role);  
    if (role === "Admin") {
      loadEvents();
    } else {
      navigate('/auth');
      return;  
    }
  }, [navigate, pageIndex, pageSize, role]);

  const validateForm = () => {
    const newErrors = {};
    if (!formData.title) newErrors.title = 'Title is required';
    if (!formData.description) newErrors.description = 'Description is required';
    if (!formData.dateTime) newErrors.dateTime = 'Date and time are required';
    if (!formData.location.address) newErrors.address = 'Address is required';
    if (!formData.location.city) newErrors.city = 'City is required';
    if (!formData.location.state) newErrors.state = 'State is required';
    if (!formData.location.country) newErrors.country = 'Country is required';
    if (formData.maxUsers <= 0) newErrors.maxUsers = 'Max users must be greater than 0';
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleDelete = async (eventId) => {
    try {
      await deleteEvent(eventId);
      setEvents(events.filter(event => event.id !== eventId)); 
    } catch (error) {
      console.error('Error deleting event:', error);
    }
  };

  const handleEdit = (event) => {
    setIsEditing(true);
    setCurrentEvent(event);
    setFormData({
      id: event ? event.id : '',
      title: event ? event.title : '',
      description: event ? event.description : '',
      dateTime: event ? event.dateTime : '',
      location: event ? event.location : { address: '', city: '', state: '', country: '' },
      category: event ? event.category : '',
      maxUsers: event ? event.maxUsers : 0,
      imageId: event ? event.imageId : '',
      imageData: event ? event.imageData : '',
      imageType: event ? event.imageType : '',
    });
    setImagePreview(event ? `data:${event.imageType};base64,${event.imageData}` : null);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setCurrentEvent(null);
    setFormData({
      id: '',
      title: '',
      description: '',
      dateTime: '',
      location: { address: '', city: '', state: '', country: '' },
      category: '',
      maxUsers: 0,
      imageId: '',
      imageData: '',
      imageType: '',
    });
    setImagePreview(null);
    setErrors({});
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name.startsWith('location.')) {
      const locationField = name.split('.')[1];
      setFormData((prev) => ({
        ...prev,
        location: {
          ...prev.location,
          [locationField]: value,
        },
      }));
    } else {
      setFormData((prev) => ({
        ...prev,
        [name]: value,
      }));
    }
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        const base64Data = reader.result.split(',')[1];
        setFormData((prevFormData) => ({
          ...prevFormData,
          imageData: base64Data,
          imageType: file.type,
        }));
        setImagePreview(reader.result);
      };
      reader.readAsDataURL(file);
    } else {
      setImagePreview(null);
    }
  };

  const handleSaveEvent = async (e) => {
    e.preventDefault();
    if (!validateForm()) {
      console.error('Validation failed');
      return;
    }

    const utcDateTime = new Date(formData.dateTime).toISOString();
    const updatedFormData = { ...formData, dateTime: utcDateTime };

    try {
      if (currentEvent) {
        await updateEvent(updatedFormData);
      } else {
        await createEvent(updatedFormData);
      }
      handleCancelEdit();
      setPageIndex(1);
    } catch (error) {
      console.error('Error saving event:', error.response?.data || error.message);
    }
  };

  const handlePageChange = (newPageIndex) => {
    if (newPageIndex > 0 && newPageIndex <= totalPages) {
      setPageIndex(newPageIndex);
    }
  };

  return (
    <div className="event-management-page">
      <h2>Event Management</h2>
      <p className="event-description">Welcome to the Event Management page! 🎉</p>
      <button onClick={() => handleEdit(null)} className="create-event-btn">Create Event</button>

      <div className="content-wrapper">
        {isEditing && (
          <form onSubmit={handleSaveEvent} className="event-form">
            <div>
              <label>Title</label>
              <input
                type="text"
                name="title"
                value={formData.title}
                onChange={handleChange}
              />
              {errors.title && <span className="error">{errors.title}</span>}
            </div>
            <div>
              <label>Description</label>
              <textarea
                name="description"
                value={formData.description}
                onChange={handleChange}
              />
              {errors.description && <span className="error">{errors.description}</span>}
            </div>
            <div>
              <label>Date and Time</label>
              <input
                type="datetime-local"
                name="dateTime"
                value={formData.dateTime}
                onChange={handleChange}
              />
              {errors.dateTime && <span className="error">{errors.dateTime}</span>}
            </div>
            <div>
              <label>Location</label>
              <div>
                <input
                  type="text"
                  name="location.address"
                  value={formData.location.address}
                  onChange={handleChange}
                  placeholder="Address"
                />
                {errors.address && <span className="error">{errors.address}</span>}
                <input
                  type="text"
                  name="location.city"
                  value={formData.location.city}
                  onChange={handleChange}
                  placeholder="City"
                />
                {errors.city && <span className="error">{errors.city}</span>}
                <input
                  type="text"
                  name="location.state"
                  value={formData.location.state}
                  onChange={handleChange}
                  placeholder="State"
                />
                {errors.state && <span className="error">{errors.state}</span>}
                <input
                  type="text"
                  name="location.country"
                  value={formData.location.country}
                  onChange={handleChange}
                  placeholder="Country"
                />
                {errors.country && <span className="error">{errors.country}</span>}
              </div>
            </div>
            <div>
              <label>Category</label>
              <input
                type="text"
                name="category"
                value={formData.category}
                onChange={handleChange}
              />
            </div>
            <div>
              <label>Max Users</label>
              <input
                type="number"
                name="maxUsers"
                value={formData.maxUsers}
                onChange={handleChange}
              />
              {errors.maxUsers && <span className="error">{errors.maxUsers}</span>}
            </div>
            <div>
              <label>Event Image</label>
              <input
                type="file"
                accept="image/*"
                onChange={handleImageChange}
              />
              {imagePreview && <img src={imagePreview} alt="Image Preview" />}
            </div>
            <button type="submit">{currentEvent ? 'Save Changes' : 'Create Event'}</button>
            <button type="button" onClick={handleCancelEdit}>Cancel</button>
          </form>
        )}

        <div className="event-list">
          {events.map((event) => (
            <div key={event.id} className="event-card">
              <h3>{event.title}</h3>
              <p>{event.description}</p>
              {event.imageData && (
                <img
                  src={`data:${event.imageType};base64,${event.imageData}`}
                  alt="Event"
                />
              )}
              <p>{new Date(event.dateTime).toLocaleString()}</p>
              <button onClick={() => handleEdit(event)}>Edit</button>
              <button onClick={() => handleDelete(event.id)}>Delete</button>
            </div>
          ))}
        </div>

        <div className="pagination">
          <button onClick={() => handlePageChange(pageIndex - 1)} disabled={pageIndex === 1}>Prev</button>
          <span>{pageIndex} / {totalPages}</span>
          <button onClick={() => handlePageChange(pageIndex + 1)} disabled={pageIndex === totalPages}>Next</button>
        </div>
      </div>
    </div>
  );
};

export default EventManagementPage;
