import { Component, OnInit } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { TodoService } from '../service/todo/todo.service';
import { HttpClient } from '@angular/common/http';
import { TodoViewModel, TodoModel } from '../service/todo/todo-model';
@Component({
  selector: 'app-todo',
  templateUrl: './todo.page.html',
  styleUrls: ['./todo.page.scss'],
})
export class TodoPage implements OnInit {
  constructor(private alertController: AlertController, private service: TodoService, private http: HttpClient) { }
  public todoList: Array<TodoModel> = [];
  private checker = false;
  public CheckTodo() {
    if (this.service.todos.length === 0) { return true; }
    return false;
  }
  onClickRB(check: boolean) {
    this.checker = check;
    this.loadDataOfPage();
  }
  async showDialogAdd() {
    const alert = await this.alertController.create({
      header: 'Add Todo',
      inputs: [
        {
          name: 'todoName',
          type: 'text',
          placeholder: 'Todo Name'
        },
        {
          name: 'description',
          type: 'text',
          placeholder: 'Description'
        },
      ],
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: () => { }
        }, {
          text: 'Ok',
          handler: (alertData) => {
            let todo: TodoViewModel;
            todo = {
              TodoName: alertData.todoName,
              Description: alertData.description,
            };
            this.service.postTodoList(todo).subscribe(() => {
              this.loadDataOfPage();
            });
          }
        }
      ]
    });
    await alert.present();
  }
  async deleteTodo(id) {
    const alert = await this.alertController.create({
      header: 'Do you want delete todo?',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: () => { }
        }, {
          text: 'Ok',
          handler: () => {
            this.service.deleteTodoItem(id).subscribe(() => {
              this.loadDataOfPage();
            });
          }
        }]
    });
    await alert.present();
  }
  doneTodo(id) {
    const data = new FormData();
    data.append('Id', id);
    const res = this.service.updateStatusTodo(data);
    this.http.request(res).subscribe(() => {
      this.loadDataOfPage();
    });
  }
  async loadDataOfPage() {
    const todoData = await this.service.getTodoList(this.checker);
    this.todoList = todoData;
  }
  ngOnInit() {
    this.loadDataOfPage();
  }
}
