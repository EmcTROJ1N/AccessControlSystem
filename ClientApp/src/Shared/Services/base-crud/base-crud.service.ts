import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Envelope} from "@Models/envelope";

@Injectable({
  providedIn: 'root'
})
export abstract class BaseCrudService<T> {
  protected constructor(protected http: HttpClient, protected endpoint: string) {}

  getAll(): Observable<Envelope> {
    return this.http.get<Envelope>(`${this.endpoint}`);
  }

  get(id: string): Observable<T> {
    return this.http.get<T>(`${this.endpoint}/${id}`);
  }

  create(item: T): Observable<T> {
    return this.http.post<T>(`${this.endpoint}`, item);
  }

  update(id: string, item: Partial<T>): Observable<T> {
    return this.http.put<T>(`${this.endpoint}/${id}`, item);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.endpoint}/${id}`);
  }
}
