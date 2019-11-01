import { Component, OnInit, Inject } from '@angular/core';
import { ImageService } from 'src/app/service/images/image.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { TodoService } from 'src/app/service/todo/todo.service';
import { LoadingController } from '@ionic/angular';
@Component({
  selector: 'app-show-add-image',
  templateUrl: './show-add-image.component.html',
  styleUrls: ['./show-add-image.component.scss'],
})
export class ShowAddImageComponent implements OnInit {
  currentFile: any;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private loadingController: LoadingController,
    private imageService: ImageService,
    private http: HttpClient, private todoService: TodoService,
    private dialogRef: MatDialogRef<ShowAddImageComponent>) { }

  ngOnInit() { }
  async upload() {
    const loading = await this.loadingController.create({
      message: 'Please Wait..',
    });
    // tslint:disable-next-line: prefer-const
    let formData = new FormData();
    for (const item of this.currentFile) {
      formData.append(item.name, item);
      formData.append('todoid', this.data);
    }
    loading.present();
    const uploadReq = this.imageService.postImage(formData);
    // tslint:disable-next-line: no-unused-expression
    this.http.request(uploadReq).subscribe((res) => {
      loading.dismiss().then(() => {
        this.todoService.getTodoList();
        this.dialogRef.close();
      });
    }, err => {
      console.log(err);
      loading.dismiss();
    });
  }
  loadFile(event) {
    this.currentFile = event.target.files;
  }
}
