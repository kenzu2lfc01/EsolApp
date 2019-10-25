import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RouterNamesService implements OnDestroy {
  public name = new Subject();
  constructor() { }
  ngOnDestroy() {
    this.name = new Subject();
  }
}
