import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Envelope} from "@Models/envelope";

@Injectable({
  providedIn: 'root'
})
export class AccessControllerService {

  constructor(protected http: HttpClient) {}

  check(LicensePlate: string, EmployeeId: string) {
    const params = new HttpParams()
      .set('LicensePlate', LicensePlate)
      .set('id', EmployeeId);
    return this.http.get<Envelope>('access', { params })
  }
}
