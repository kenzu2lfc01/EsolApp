import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TodoService } from 'src/app/service/todo/todo.service';
import { TodoModel } from 'src/app/service/todo/todo-model';
import { Router } from '@angular/router';
import { RouterNamesService } from 'src/app/service/router-names.service';

@Component({
  selector: 'app-shared-todo',
  templateUrl: './shared-todo.component.html',
  styleUrls: ['./shared-todo.component.scss'],
})
export class SharedTodoComponent implements OnInit {

  constructor(private service: TodoService, private router: Router, private routerNameService: RouterNamesService) { }
  public todoList: Array<TodoModel> = [];

  ngOnInit() { }
  loadDataOfPage() {
    this.service.getTodoShareList().then((res) => {
      this.todoList = res;
    });
  }
  openMenuUpload(TodoId) {
    this.router.navigate(['/tododetail', {id: TodoId, checker: true}]);
  }
  ionViewWillEnter() {
    this.routerNameService.name.next('Todo Share');
    this.loadDataOfPage();
  }
}
