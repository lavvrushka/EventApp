# Event Management Web Application

# Setup and Installation
### Clone the repository
- git clone https://github.com/lavvrushka/EventApp.git
- cd EventApp
- docker-compose up --build
- localhost:3000
### Admin 
- email: ivanlavrivwork@gmail.com
- password: 1234567
  
## Overview
This is a web application developed using .NET Core and EF Core for event management. The application provides full CRUD functionality for events and allows users to register and participate in events. The client-side is built using React, with full integration with the Web API for seamless interaction.

## Features
### Web API Functionality
**Event Management:**
- Retrieve a list of all events
- Retrieve an event by ID
- Retrieve an event by name
- Add a new event
- Update existing event information
- Delete an event
- Retrieve events by date, location, or category
- Upload and store images for events

**Participant Management:**
- User registration for events
- Retrieve a list of event participants
- Retrieve a participant by ID
- Cancel user registration for an event
- Send notifications to participants about event updates
  ![image](https://github.com/user-attachments/assets/1c60e9e0-eb67-46fd-8e16-19abd886f02d)

### Client-Side Application (React)
- User authentication/registration page
  ![image_2024-12-30_04-05-17](https://github.com/user-attachments/assets/469a0680-5df1-4edf-a44c-a55db4dee4b2)
- Event list page with availability status
  ![image_2024-12-30_03-30-46](https://github.com/user-attachments/assets/015626d9-d7d0-4c60-aab8-f8495ed36f5c)
- Detailed event information page
  ![image_2024-12-30_03-31-42](https://github.com/user-attachments/assets/4131f83a-364f-4f7a-afb4-9caf9bff7ccf)
- Event registration page
  ![image_2024-12-30_03-32-10](https://github.com/user-attachments/assets/a681bd31-a28b-4d54-9624-6d58b491039f)
- User's registered events page
  ![image_2024-12-30_03-32-42](https://github.com/user-attachments/assets/8c64e894-c901-43c6-93de-d1f496392f37)
- Pagination, search, and filtering by category/location
  ![image_2024-12-30_03-29-35](https://github.com/user-attachments/assets/88ca4647-7578-4732-ab60-170dba6f9460)
  ![image](https://github.com/user-attachments/assets/cd54f67b-f12b-4ecd-a474-6e3259c77ee9)
- Admin section for managing events (create, edit, delete)
  ![image_2024-12-30_03-32-26](https://github.com/user-attachments/assets/61469071-e685-4dfd-9c45-5b1ba17cb9a3)


## Technical Requirements
- .NET 5.0+
- Entity Framework Core
- MS SQL/PostgreSQL
- AutoMapper / Mapster
- FluentValidation
- JWT access and refresh token authentication (e.g., IdentityServer4)
- Swagger for API documentation
- EF Fluent API
- React
- xUnit/nUnit for testing

## Architecture
The project follows a Clean Architecture approach to ensure separation of concerns and scalability.

## Database schema
- PostgreSQL
  ![image_2024-12-30_07-42-26](https://github.com/user-attachments/assets/741f1ffc-dbcb-4782-bed9-787bb5465460)
