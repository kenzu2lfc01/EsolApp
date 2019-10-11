import { Injectable } from '@angular/core';
import { TodoModel, TodoViewModel } from './todo-model';
import { HttpClient, HttpRequest } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  public todos: Array<TodoModel> = [];
  constructor(private http: HttpClient) { }
  readonly rootUrl = 'https://localhost:44346/api';

  getTodoList() {
    this.http.get(this.rootUrl + `/Todo`).toPromise().then(res => this.todos = res as TodoModel[]);
  }
  postTodoList(todo: TodoViewModel) {
    this.http.post<TodoViewModel>(this.rootUrl + `/Todo`, todo);
  }
}
