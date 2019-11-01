import { Injectable } from '@angular/core';
import { TodoModel, TodoViewModel, TodoPatchViewModel } from './todo-model';
import { HttpClient, HttpRequest, HttpHeaders } from '@angular/common/http';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
@Injectable({
  providedIn: 'root'
})
export class TodoService {
  constructor(private http: HttpClient) { }
  public todos: Array<TodoModel> = [];
  // tslint:disable-next-line: max-line-length
  httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json', Authorization: localStorage.getItem('token') }), responseType: 'text' as 'json' };
  rootUrl = 'https://localhost:44346/api/Todo';
  todoViewModel: TodoPatchViewModel;
  async getTodoList() {
    await this.http.get(this.rootUrl + '/get', this.httpOptions).toPromise().then((res: string) => this.todos = JSON.parse(res));
    return await this.todos;
  }
  postTodoList(todo) {
    return this.http.post(this.rootUrl + `/create`, todo, this.httpOptions);
  }
  deleteTodoItem(id) {
    return this.http.delete(this.rootUrl + '/delete/' + id);
  }
  updateStatusTodo(data) {
    const httpRequest = new HttpRequest('POST', this.rootUrl + `/update`, data);
    return httpRequest;
  }
  updateTodo(todo: TodoModel) {
    this.todoViewModel = {
      id: todo.id,
      TodoName: todo.todoName,
      Description: todo.description
    };
    return this.http.put(this.rootUrl, this.todoViewModel, this.httpOptions);
  }
}
