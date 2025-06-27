import { Injectable } from '@angular/core';
import { fromEvent, map, merge, Observable, of, shareReplay } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class OnlineStatusService {
  private online$ = merge(
    of(navigator.onLine),
    fromEvent(window, 'online').pipe(map(() => true)),
    fromEvent(window, 'offline').pipe(map(() => false))
  ).pipe(shareReplay(1));

  get isOnline$(): Observable<boolean> {
    return this.online$;
  }
}
