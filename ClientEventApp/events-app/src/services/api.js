import axios from 'axios';

const API = axios.create({
  baseURL: 'http://localhost:7145/api',
  headers: { 'Content-Type': 'application/json' },
});

API.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('accessToken'); 
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`; 
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);


API.interceptors.response.use(
  (response) => response, 
  async (error) => {
    const originalRequest = error.config;

    if (error.response && error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const refreshToken = localStorage.getItem('refreshToken'); 

        if (!refreshToken) {
          console.error('Refresh token is missing');
          throw new Error('Refresh token is missing');
        }

        const response = await API.post('/user/refresh-token', { refreshToken });
        localStorage.setItem('accessToken', response.data.accessToken);
        localStorage.setItem('refreshToken', response.data.refreshToken);

        originalRequest.headers['Authorization'] = `Bearer ${response.data.accessToken}`;
        return API(originalRequest);
      } catch (refreshError) {
        console.error('Ошибка при обновлении токенов:', refreshError);
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export const refreshToken = async () => {
    try {
      const refreshToken = localStorage.getItem('refreshToken'); 
      if (!refreshToken) {
        throw new Error('Refresh token is missing');
      }
  
      const response = await API.post('/user/refresh-token', { refreshToken });
      localStorage.setItem('accessToken', response.data.accessToken);
      localStorage.setItem('refreshToken', response.data.refreshToken);
  
      console.log('Токены обновлены:', response.data);
  
      return response.data; 
    } catch (error) {
      console.error('Ошибка при обновлении токенов:', error);
      throw error;
    }
  };

export const getAllEvents = async (pageIndex = 1, pageSize = 2) => {
  try {
    const response = await API.get(`/events/page?PageIndex=${pageIndex}&PageSize=${pageSize}`);
    console.log('API Response:', response.data); 

    if (Array.isArray(response.data.items)) {
      return response.data;
    } else {
      throw new Error('Полученные данные не являются массивом');
    }
  } catch (error) {
    console.error('Ошибка при получении событий:', error);
    throw error;
  }
};

export const getEventById = async (id) => {
  try {
    const response = await API.get(`/events/by-id/${id}`);
    return response.data;
  } catch (error) {
    console.error('Ошибка при получении события по ID:', error);
    throw error;
  }
};

export const createEvent = async (eventData) => {
  try {
    await API.post('/events/create', eventData);
  } catch (error) {
    console.error('Ошибка при создании события:', error);
    throw error;
  }
};


export const updateEvent = async (updatedData) => {
  try {
    await API.put(`/events/update`, updatedData);
  } catch (error) {
    console.error('Ошибка при обновлении события:', error);
    throw error;
  }
};

export const deleteEvent = async (id) => {
  try {
    await API.delete(`/events/delete/${id}`);
  } catch (error) {
    console.error('Ошибка при удалении события:', error);
    throw error;
  }
};


export const filterEvents = async (params) => {
  try {
    const response = await API.get('/events/filter', { params });
    return response.data;
  } catch (error) {
    console.error('Ошибка при фильтрации событий:', error);
    throw error;
  }
};


export const getEventsByDate = async (date) => {
  try {
    const response = await API.get('/events/by-date', { params: { date } });
    return response.data;
  } catch (error) {
    console.error('Ошибка при получении событий по дате:', error);
    throw error;
  }
};


export const getEventsByTitle = async (title) => {
  try {
    const response = await API.get('/events/by-title', { params: { title } });
    return response.data;
  } catch (error) {
    console.error('Ошибка при получении событий по названию:', error);
    throw error;
  }
};

export const loginUser = async (userData) => {
    try {
      console.log('Попытка логина пользователя:', userData);
      const response = await API.post('/user/login', userData);

      localStorage.setItem('accessToken', response.data.accessToken);
      localStorage.setItem('refreshToken', response.data.refreshToken);
      console.log('Токены сохранены в localStorage:', response.data.accessToken, response.data.refreshToken);
  
      const currentUser = await getCurrentUser();
      console.log('Текущий пользователь:', currentUser);
      localStorage.setItem('role', JSON.stringify(currentUser.roleName));
      localStorage.setItem('user', JSON.stringify(currentUser));
      console.log('Данные пользователя сохранены:', currentUser.id, currentUser.roleName);
  
      return response.data;
    } catch (error) {
      console.error('Ошибка при логине пользователя:', error);
      throw error;
    }
  };
  
  export const registerUser = async (userData) => {
    try {
      console.log("Отправка данных для регистрации:", userData); 
      const response = await API.post('/user/register', userData);
      localStorage.setItem('accessToken', response.data.accessToken);
      localStorage.setItem('refreshToken', response.data.refreshToken);
      console.log('Токены сохранены в localStorage:', response.data.accessToken, response.data.refreshToken);
  
      const currentUser = await getCurrentUser();
      console.log('Текущий пользователь после регистрации:', currentUser);
  
      localStorage.setItem('user', JSON.stringify(currentUser));
      console.log('Данные пользователя сохранены:', currentUser.id, currentUser.roleName);
  
      return response.data;
    } catch (error) {
      console.error('Ошибка при регистрации пользователя:', error);
      if (error.response) {
        console.error('Ответ сервера:', error.response.data);
        alert(`Ошибка регистрации: ${error.response.data.message || error.response.statusText}`);
      } else {
        alert('Ошибка отправки запроса. Пожалуйста, попробуйте снова позже.');
      }
      throw error;
    }
  };
  
export const logoutUser = async () => {
    try {
      await API.post('/user/logout');
      localStorage.removeItem('accessToken'); 
      localStorage.removeItem('userId'); 
    } catch (error) {
      console.error('Error during user logout:', error);
      throw error;
    }
  };

export const deleteUserFromEvent = async (requestData) => {
  try {
    await API.delete('/user/remove-from-event', { data: requestData });
  } catch (error) {
    console.error('Ошибка при удалении пользователя из события:', error);
    throw error;
  }
};

export const getUserEvents = async (userId) => {
  try {
    const token = localStorage.getItem('accessToken');
    const response = await API.get('/user/events', {
      params: { userId }, 
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    console.log('API Response:', response.data); 
    
    if (Array.isArray(response.data)) {
      return response.data; 
    } else if (response.data.items && Array.isArray(response.data.items)) {
      return response.data.items; 
    } else {
      throw new Error('Неверная структура данных');
    }
  } catch (error) {
    console.error('Ошибка при получении событий пользователя:', error);
    throw error;
  }
};

export const getUsersByEvent = async (eventId) => {
  try {
    const response = await API.get(`/user/event-users/${eventId}`);
    return response.data;
  } catch (error) {
    console.error('Ошибка при получении пользователей события:', error);
    throw error;
  }
};


export const registerUserToEvent = async (requestData, token) => {
  try {
    console.log(requestData);
    const response = await API.post('/user/register-to-event', requestData, {
      headers: {
        Authorization: `Bearer ${token}`,  
      },
    });
    return response.data;
  } catch (error) {
    console.error('Ошибка при регистрации пользователя на событие:', error.response?.data || error.message);
    throw new Error(error.response?.data?.details || error.message); 
  }
};

export const getUsersByPage = async (pageIndex, pageSize) => {
  try {
    const response = await API.get('/user/paginate-users', { params: { PageIndex: pageIndex, PageSize: pageSize } });
    return response.data;
  } catch (error) {
    console.error('Ошибка при получении пользователей с пагинацией:', error);
    throw error;
  }
};

export const getCurrentUser = async () => {
    try {
      const response = await API.get('/user/current');
      return response.data;
    } catch (error) {
      console.error('Ошибка при получении текущего пользователя:', error);
      throw error;
    }
};