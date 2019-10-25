import { Component, OnInit } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { TodoService } from '../service/todo/todo.service';
import { HttpClient } from '@angular/common/http';
import { TodoViewModel, TodoModel } from '../service/todo/todo-model';
import { RouterNamesService } from '../service/router-names.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ImageService } from '../service/images/image.service';
import { MatDialog } from '@angular/material';
import { DialogComponent } from './image/dialog/dialog.component';
@Component({
  selector: 'app-todo',
  templateUrl: './todo.page.html',
  styleUrls: ['./todo.page.scss'],
})
export class TodoPage implements OnInit {
  // tslint:disable-next-line: max-line-length
  constructor(private alertController: AlertController,
    private service: TodoService,
    private http: HttpClient,
    private routerNameService: RouterNamesService,
    private imageService: ImageService,
    private sanitizer: DomSanitizer,
    private dialog: MatDialog) {
  }

  // tslint:disable-next-line: member-ordering
  public todoList: Array<TodoModel> = [];
  private checker = false;
  ionViewWillEnter() {
    this.checker = false;
    this.routerNameService.name.next('Todo');
    this.loadDataOfPage();
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
  loadDataOfPage() {
    this.service.getTodoList().then((res) => {
      this.todoList = res.filter(x => x.status === this.checker);
      console.log(this.todoList);
    });
  }
  public CheckTodo() {
    if (this.service.todos.length === 0) { return true; }
    return false;
  }
  ngOnInit() {
  }
  openNewTab(temp: string) {
    window.open('http://localhost:8100' + temp, '_blank');
  }
  openMenuUpload(id) {
    this.dialog.open(DialogComponent, { data: { todoId: id, checker: this.checker } });
  }
}
