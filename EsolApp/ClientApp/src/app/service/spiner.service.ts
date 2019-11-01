import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinerService {
  private showSpinner = new Subject();
  getData() {
    return this.showSpinner;
  }
  activate() {
    this.showSpinner.next(true);
  }
  deactivate() {
    this.showSpinner.next(false);
  }
  constructor() { }
}
