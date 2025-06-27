import { HttpClient } from '@angular/common/http';
import {
  Observable,
  from,
  of,
  switchMap,
  map,
  catchError,
  tap,
  concatMap,
  toArray,
} from 'rxjs';
import Dexie from 'dexie';
import { inject, Injectable } from '@angular/core';
import { LocalStores } from '../interfaces/local-stores-injection-token';
import { OnlineStatusService } from './online-status-service';
import { DatabaseInitializerService } from './database-initializer-service';
import { Transaction } from '../data/transaction';

@Injectable({
  providedIn: 'root',
})
export class DataService<T> {
  private readonly db = new Dexie('nutrifit');
  private readonly transactionTableName = 'transactions';
  private readonly httpClient = inject(HttpClient);
  private readonly databaseInitializerService = inject(
    DatabaseInitializerService
  );
  private readonly stores = inject(LocalStores);
  private readonly onlineStatusService = inject(OnlineStatusService);

  private isOnline = true;

  constructor() {
    from(
      this.databaseInitializerService.initializeDatabase(
        this.db,
        this.transactionTableName,
        this.stores
      )
    )
      .pipe(
        switchMap(() =>
          this.onlineStatusService.isOnline$.pipe(
            tap((status) => (this.isOnline = status)),
            switchMap((status) => (status ? this.syncTransactions() : of(null)))
          )
        )
      )
      .subscribe();
  }

  public getData<T>(
    collection: string,
    key: string,
    endpoint: string,
    params?: Record<string, string>
  ): Observable<T | null> {
    return this.getLocalData<T>(collection, key).pipe(
      switchMap((localData) =>
        this.isOnline
          ? this.fetchFromApiAndStore<T>(
              collection,
              key,
              endpoint,
              params
            ).pipe(catchError(() => of(localData)))
          : of(localData)
      )
    );
  }

  private fetchFromApiAndStore<T>(
    collection: string,
    key: string,
    endpoint: string,
    params?: Record<string, string>
  ): Observable<T> {
    return this.httpClient
      .get<T>(endpoint, { params })
      .pipe(
        switchMap((data) =>
          this.setLocalData(collection, key, data).pipe(map(() => data))
        )
      );
  }

  public postData<T>(
    collection: string,
    endpoint: string,
    payload: any
  ): Observable<T> {
    const request$ = this.httpClient.post<T>(endpoint, payload);
    return this.isOnline
      ? request$.pipe(
          switchMap((response) =>
            this.saveTransaction({
              collection: collection,
              endpoint: endpoint,
              method: 'POST',
              payload: payload,
              data: response,
            } as Transaction<T>).pipe(map(() => response))
          )
        )
      : this.saveTransaction({
          collection: collection,
          payload: payload,
          method: 'POST',
          endpoint: endpoint,
        } as Transaction<T>);
  }

  public putData<T>(
    collection: string,
    endpoint: string,
    key: string,
    data: any
  ): Observable<T> {
    const request$ = this.httpClient.put<T>(endpoint, data);
    return this.isOnline
      ? request$.pipe(
          switchMap((response) =>
            this.saveTransaction(
              collection,
              key,
              response,
              'PUT',
              endpoint,
              data
            ).pipe(map(() => response))
          )
        )
      : this.saveTransaction(collection, key, data, 'PUT', endpoint);
  }

  public deleteData<T>(
    collection: string,
    endpoint: string,
    key: string
  ): Observable<T | null> {
    const request$ = this.httpClient.delete<T>(endpoint);
    return this.isOnline
      ? request$.pipe(
          switchMap((response) =>
            this.saveTransaction(
              collection,
              key,
              response,
              'DELETE',
              endpoint
            ).pipe(map(() => response))
          )
        )
      : this.saveTransaction(collection, key, null, 'DELETE', endpoint);
  }

  private setLocalData<T>(
    collection: string,
    key: string,
    data: T
  ): Observable<T> {
    return from(
      this.db.table(collection).put({ key, value: JSON.stringify(data) })
    ).pipe(map(() => data));
  }

  private getLocalData<T>(
    collection: string,
    key: string
  ): Observable<T | null> {
    return from(this.db.table(collection).get({ key })).pipe(
      map((record) => (record?.value ? (JSON.parse(record.value) as T) : null)),
      catchError((error) => {
        console.error('Error getting data from local storage', error);
        return of(null);
      })
    );
  }

  private clearData(collection: string, key: string): Observable<void> {
    return from(this.db.table(collection).delete(key));
  }

  private clearAllData(collection: string): Observable<void> {
    return from(this.db.table(collection).clear());
  }

  private saveTransaction<T>(
    transaction: Transaction<T>
  ): Observable<T | null> {
    return from(
      this.db.table<Transaction<T>>(this.transactionTableName).add(transaction)
    ).pipe(map(() => transaction.data));
  }

  private syncTransactions(): Observable<void> {
    return from(
      this.db.table<Transaction<T>>(this.transactionTableName).toArray()
    ).pipe(
      switchMap((transactions) =>
        from(transactions).pipe(
          concatMap((transaction) => {
            let request$: Observable<any>;

            if (transaction.method === 'POST') {
              request$ = this.httpClient.post(
                transaction.endpoint,
                transaction.payload
              );
            } else if (transaction.method === 'PUT') {
              request$ = this.httpClient.put(
                transaction.endpoint,
                transaction.payload
              );
            } else {
              request$ = this.httpClient.delete(transaction.endpoint);
            }

            return request$.pipe(
              switchMap(() =>
                from(
                  this.db
                    .table<Transaction<T>>(this.transactionTableName)
                    .delete(transaction.id as number)
                )
              ),
              catchError((error) => {
                console.error('Error syncing transaction', error);
                return of(null);
              })
            );
          }),
          toArray(),
          map(() => void 0)
        )
      )
    );
  }
}
