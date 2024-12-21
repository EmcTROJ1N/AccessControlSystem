import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '@Services/employee';
import { Employee } from '@Models/employee';
import { ButtonComponent } from 'Shared/Components/button';
import { ListComponent } from 'Widgets/list';
import {EmployeeListItemComponent} from "Entities/employee-list-item";
import {NgForOf} from "@angular/common";
import {EmployeeDetailsComponent} from "Widgets/employee-details";

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  standalone: true,
  imports: [
    FormsModule,
    ButtonComponent,
    ListComponent,
    EmployeeListItemComponent,
    NgForOf,
    EmployeeDetailsComponent
  ],
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  employees: Employee[] = [];
  currentEmployee: Employee = new Employee();
  currentIndex = -1;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.retrieveEmployees();
  }

  retrieveEmployees(): void {
    this.employeeService.getAll().subscribe({
      next: (data) => {
        this.employees = data.Result;
        console.log('Employees retrieved:', data);
      },
      error: (e) => console.error('Error fetching employees:', e)
    });
  }

  refreshList(): void {
    this.retrieveEmployees();
    this.currentEmployee = new Employee();
    this.currentIndex = -1;
  }

  setActiveEmployee(employee: Employee, index: number): void {
    this.currentEmployee = employee;
    this.currentIndex = index;
  }
}
