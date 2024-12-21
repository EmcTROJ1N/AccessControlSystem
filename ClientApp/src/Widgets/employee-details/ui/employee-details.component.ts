import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import { EmployeeService } from 'Shared/Services/employee';
import { Employee } from 'Shared/Models/employee';
import { ButtonComponent } from 'Shared/Components/button';
import {Envelope} from "@Models/envelope";

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  standalone: true,
  imports: [
    FormsModule,
    RouterLink,
    ButtonComponent
  ],
  styleUrls: ['./employee-details.component.css']
})
export class EmployeeDetailsComponent implements OnInit {
  @Input() viewMode = false;
  @Input() currentEmployee: Employee = new Employee();

  message = '';

  constructor(
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      this.getEmployee(this.route.snapshot.params['id']);
    }
  }

  getEmployee(id: string): void {
    this.employeeService.get(id).subscribe({
      next: (data) => {
        this.currentEmployee = data;
        console.log('Employee fetched:', data);
      },
      error: (e) => console.error('Error fetching employee:', e)
    });
  }

  updateEmployee(): void {
    if (this.currentEmployee.Id == null) return;
    this.message = '';

    this.employeeService.update(this.currentEmployee.Id, this.currentEmployee).subscribe({
      next: (res) => {
        console.log('Employee updated:', res);
        this.message = 'The employee was updated successfully!';
      },
      error: (e) => console.error('Error updating employee:', e)
    });
  }

  deleteEmployee(): void {
    if (this.currentEmployee.Id == null) return;

    this.employeeService.delete(this.currentEmployee.Id).subscribe({
      next: (res) => {
        this.message = `Employee deleted, reload page`;
        this.router.navigate(['/employees']);
      },
      error: (e) => console.error('Error deleting employee:', e)
    });
  }
}
