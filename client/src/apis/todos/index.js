class Todos {
  constructor(instance) {
    this.client = instance;
  }

  get() {
    return this.client.get('/api/todos');
  }
}

export default Todos;
