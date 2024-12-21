import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Car} from "Shared/Models/car";
import {BaseCrudService} from "@Services/base-crud";

@Injectable({
  providedIn: 'root',
})
export class CarService extends BaseCrudService<Car> {

  constructor(http: HttpClient) {
    super(http, 'car');
  }

  deleteAll(): Observable<any> {
    return this.http.delete(this.endpoint);
  }

  findByLicensePlate(licensePlate: string): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.endpoint}/search?licensePlate=${licensePlate}`);
  }
}
