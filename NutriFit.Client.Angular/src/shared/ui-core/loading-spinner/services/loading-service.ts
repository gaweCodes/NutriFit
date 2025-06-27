import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private requestCount = 0;
  public loading$: Observable<boolean> = this.loadingSubject.asObservable();

  public show(): void {
    this.requestCount++;
    this.loadingSubject.next(true);
  }

  public hide(): void {
    this.requestCount--;
    if (this.requestCount === 0) {
      this.loadingSubject.next(false);
    }
  }
}
