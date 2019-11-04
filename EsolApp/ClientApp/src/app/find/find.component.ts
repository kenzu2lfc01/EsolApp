import { Component, OnInit, Inject } from '@angular/core';
import { LoginService } from '../service/auth/login.service';
import { } from '../service/auth/login-model.model';
import { MAT_DIALOG_DATA } from '@angular/material';
import { TodoService } from '../service/todo/todo.service';
import { LoginShareViewModel } from '../service/todo/todo-model';

@Component({
  selector: 'app-find',
  templateUrl: './find.component.html',
  styleUrls: ['./find.component.scss'],
})
export class FindComponent implements OnInit {
  constructor(private accountService: LoginService, private todoService: TodoService, @Inject(MAT_DIALOG_DATA) public data: any) { }
  idUsers: Array<string> = [];
  private isHidden = true;
  todoShare: LoginShareViewModel;
  ngOnInit() {
    this.accountService.getUsers();
  }
  onShare() {
    this.todoShare = {
      UserId: this.idUsers,
      TodoId: this.data
    };
    if (this.todoShare.UserId === null) {
      alert('Please choose people to share!!');
    } else {
      this.todoService.onShare(this.todoShare).subscribe(() => {
        alert('Share success');
        this.isHidden = true;
      },
        err => {
          console.log(err);
        });
    }
  }
  onChoose(id) {
    if (this.isHidden === true) {
      this.isHidden = false;
      this.idUsers.push(id);
    } else {
      const index = this.idUsers.indexOf(id);
      this.idUsers.splice(index, 1);
      this.isHidden = true;
    }
  }
}
