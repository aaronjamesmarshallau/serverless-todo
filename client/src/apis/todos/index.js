class Todos {
  constructor(instance) {
    this.client = instance;
  }

  get() {
    return this.client.get('/api/todos');
  }

  create(item) {
    return this.client.post('/api/todos', item);
  }
}

export default Todos;
