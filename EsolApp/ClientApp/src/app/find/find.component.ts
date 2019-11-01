import { Component, OnInit } from '@angular/core';
import { LoginService } from '../service/auth/login.service';

@Component({
  selector: 'app-find',
  templateUrl: './find.component.html',
  styleUrls: ['./find.component.scss'],
})
export class FindComponent implements OnInit {

  constructor(private accountService: LoginService) { }
  private isHidden = true;
  ngOnInit() {
    this.accountService.getUsers();
  }
  onChoose() {
    if (this.isHidden === true) {
      this.isHidden = false;
    } else {
      this.isHidden = true;
    }
  }
}
