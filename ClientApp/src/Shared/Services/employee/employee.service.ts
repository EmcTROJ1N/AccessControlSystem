import { Injectable } from '@angular/core';
import {BaseCrudService} from "@Services/base-crud";
import {Employee} from "Shared/Models/employee";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends BaseCrudService<Employee> {
  constructor(http: HttpClient) {
    super(http, 'employees');
  }
}
