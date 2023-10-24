
import axios from "axios"
import { useNavigate } from "react-router-dom";


const axiosInstance = axios.create({
    baseURL: 'http://localhost:8443/'
  });


axiosInstance.interceptors.request.use(config => {
  const token = getAuthUser()
  config.headers.Authorization = `Bearer ${token}` // you may need "Bearer" here
  return config
},(error) => {
    Promise.reject(error);
  })

const getAuthUser = () => {
    return JSON.parse(localStorage.getItem('token'));
}  

// Response interceptor for API calls
axiosInstance.interceptors.response.use((response) => {
    return response
  }, function (error) {
    if (error.response.status === 401) {
      alert("You have been logged out due to inactivity")
      window.location.href = '/login';
    }
    return Promise.reject(error);
  });


export default axiosInstance;