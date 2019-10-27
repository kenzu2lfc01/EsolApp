import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ImageService } from 'src/app/service/images/image.service';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { TodoService } from 'src/app/service/todo/todo.service';
@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements OnInit {

  // tslint:disable-next-line: max-line-length
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialogRef<DialogComponent>, private todoService: TodoService, private sinper: NgxSpinnerService, private imageService: ImageService, private http: HttpClient) { }
  currentFile: any;
  message = 'Upload successs';
  ngOnInit() {
  }

  upload() {
    // tslint:disable-next-line: prefer-const
    let formData = new FormData();
    for (const item of this.currentFile) {
      formData.append(item.name, item);
      formData.append('todoid', this.data.todoId);
    }
    const uploadReq = this.imageService.postImage(formData);
    this.http.request(uploadReq).subscribe(res => {
      if (res.type === HttpEventType.UploadProgress) {
        this.sinper.show();
      }
    }, err => {
      this.message = err;
    });
    setTimeout(() => {
      this.dialogRef.close();
      this.todoService.getTodoList();
      alert(this.message)
    }, 3000);
  }
  loadFile(event) {
    this.currentFile = event.target.files;
  }
}
