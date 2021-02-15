/* eslint-disable no-console */
import axios from 'axios';
import User from '../store/user';

const instance = axios.create({
  baseURL: process.env.VUE_APP_HOST,
});

instance.interceptors.request.use(config => {
  if (User.hasToken()) {
    config.headers.Authorization = 'Bearer ' + User.token();
  }
  return config;
});

instance.interceptors.response.use(res => {
  return res;
});

export default instance;
