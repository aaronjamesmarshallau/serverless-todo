import axios from 'axios';
import Todos from './todos';

const instance = axios.create({
  baseURL: 'https://serverless-todo-api.i18u.me/',
  timeout: 30000,
});

const TodoApi = new Todos(instance);

export {
  // eslint-disable-next-line
  TodoApi,
};
