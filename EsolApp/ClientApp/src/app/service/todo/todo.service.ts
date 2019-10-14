import { Injectable } from '@angular/core';
import { TodoModel, TodoViewModel } from './todo-model';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  public todos: Array<TodoModel> = [];
  constructor(private http: HttpClient) { }
  readonly rootUrl = 'https://localhost:44346/api/Todo';

  getTodoList(checker: boolean) {
    this.http.get(this.rootUrl).toPromise().then(res => this.todos = res as TodoModel[]);
    return this.todos.filter(x => x.status === checker);
  }
  postTodoList(todo) {
    return this.http.post(this.rootUrl + `/create`, todo);
  }
  deleteTodoItem(id) {
    return this.http.delete(this.rootUrl + '/delete/' + id);
  }
  updateStatusTodo(data) {
    const httpRequest = new HttpRequest('POST', this.rootUrl + `/update`, data);
    return httpRequest;
  }
}
