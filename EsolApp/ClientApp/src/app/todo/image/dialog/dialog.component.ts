import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
// import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ImageService } from 'src/app/service/images/image.service';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { TodoService } from 'src/app/service/todo/todo.service';
import { RouterNamesService } from 'src/app/service/router-names.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TodoModel } from 'src/app/service/todo/todo-model';
import { ImageModel } from 'src/app/service/images/image.model';
import { MatDialog } from '@angular/material';
import { ShowAddImageComponent } from './show-add-image/show-add-image.component';
import { AlertController } from '@ionic/angular';
import { FindComponent } from 'src/app/find/find.component';
@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements OnInit {
  // tslint:disable-next-line: max-line-length
  constructor(
    private routerNameService: RouterNamesService,
    private todoService: TodoService,
    private imageService: ImageService,
    private route: ActivatedRoute,
    private alertController: AlertController,
    private dialog: MatDialog) {
  }
  todoItem: TodoModel = {
    id: 0,
    todoName: '',
    description: '',
    modifyDate: new Date(),
    imageViewModels: [],
    status: true,
  };
  currentFile: any;
  message = 'Upload successs';
  isEditName = true;
  isEditDes = true;
  ngOnInit() {
  }
  getRealStatus(status: boolean) {
    if (status === true) {
      return 'Done';
    }
    return 'Doing';
  }
  ionViewWillEnter() {
    this.routerNameService.name.next('Todo Detail');
    // tslint:disable-next-line: radix
    const id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    this.todoService.getTodoList().then((res) => {
      this.todoItem = res.filter(x => x.id === id)[0];
    });
  }
  showAddImageDialog() {
    this.dialog.open(ShowAddImageComponent, { data: this.todoItem.id });
  }
  showFindDialog() {
    this.dialog.open(FindComponent, { panelClass: 'custom-dialog-container' });
  }
  async deleteImage(id) {
    const alert = await this.alertController.create({
      header: 'Do you want delete image?',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: () => { }
        }, {
          text: 'Ok',
          handler: () => {
            this.imageService.deleteImage(id).subscribe(() => {
              this.todoService.getTodoList();
            });
          }
        }]
    });
    await alert.present();
  }
  onChangeInputName(value) {
    this.todoItem.todoName = value;
  }
  onChangeInputDes(value) {
    this.todoItem.description = value;
  }
  onClickEditName(inputTodoname) {
    this.isEditName = false;
    inputTodoname.setFocus();
  }
  onClickEditDes(inputDescription) {
    this.isEditDes = false;
    inputDescription.setFocus();
  }
  onFocusOutDes() {
    this.isEditDes = true;
    this.todoService.updateTodo(this.todoItem).subscribe(() => { },
      err => {
        console.log(err);
      }
    );
  }
  onFocusOutTodoName() {
    this.isEditName = true;
    this.todoService.updateTodo(this.todoItem).subscribe(() => { },
      err => {
        console.log(err);
      }
    );
  }
}
