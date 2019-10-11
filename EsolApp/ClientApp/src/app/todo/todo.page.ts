import { Component, OnInit } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { TodoService } from '../service/todo/todo.service';
import { HttpClient } from '@angular/common/http';
import { TodoViewModel } from '../service/todo/todo-model';
@Component({
  selector: 'app-todo',
  templateUrl: './todo.page.html',
  styleUrls: ['./todo.page.scss'],
})
export class TodoPage implements OnInit {
  constructor(private alertController: AlertController, private service: TodoService, private http: HttpClient) { }
  public CheckTodo() {
    if (this.service.todos.length === 0) { return true; }
    this.service.todos.forEach(e => {
      if (e.status === true) {
        return true;
      }
    });
    return false;
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
          handler: () => {
          }
        }, {
          text: 'Ok',
          handler: (alertData) => {
            let todo: TodoViewModel;
            todo = {
              name: alertData.todoName,
              description: alertData.description,
              status: false
            };
            this.service.postTodoList(todo);
          }
        }
      ]
    });

    await alert.present();
  }

  ngOnInit() {
    this.service.getTodoList();
  }

}
