import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ErrorsService {
  private isErrorSubject = new BehaviorSubject<boolean>(false);
  isError$: Observable<boolean> = this.isErrorSubject.asObservable();
  errors: string[] = [];

  constructor() {}

  add(error: string) {
    this.isErrorSubject.next(true);
    this.errors.push(error);
  }

  clear() {
    this.errors = [];
    this.isErrorSubject.next(false);
  }
}
