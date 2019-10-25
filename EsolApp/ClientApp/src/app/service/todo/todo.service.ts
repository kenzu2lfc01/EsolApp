import { Injectable } from '@angular/core';
import { TodoModel, TodoViewModel } from './todo-model';
import { HttpClient, HttpRequest, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  public todos: Array<TodoModel> = [];
  httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json', }), responseType: 'text' as 'json' };
  constructor(private http: HttpClient) { }
  rootUrl = 'https://localhost:44346/api/Todo';
  async getTodoList() {
    await this.http.get(this.rootUrl, this.httpOptions).toPromise().then((res: string) => this.todos = JSON.parse(res));
    return await this.todos;
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
