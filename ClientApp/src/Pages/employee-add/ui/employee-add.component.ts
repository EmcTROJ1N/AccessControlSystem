import { Component } from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {EmployeeService} from "@Services/employee";
import {ButtonComponent} from "Shared/Components/button";
import {InputGroupComponent} from "Shared/Components/input-group";

@Component({
  selector: 'app-employee-add',
  templateUrl: './employee-add.component.html',
  standalone: true,
  imports: [
    ButtonComponent,
    FormsModule,
    InputGroupComponent,
    ReactiveFormsModule
  ],
  styleUrl: './employee-add.component.css'
})
export class EmployeeAddComponent {
  submitted = false;
  form: FormGroup;

  constructor(private employeeService: EmployeeService, private fb: FormBuilder) {
    this.form = this.fb.group({
      FullName: ['', Validators.required],
      Department: ['', Validators.required],
    });
  }

  saveEmployee(): void {
    this.employeeService.create(this.form.value).subscribe({
      next: (res) => {
        console.log(res);
        this.submitted = true;
      },
      error: (e) => console.error(e),
    });
  }

  newEmployee(): void {
    this.submitted = false;
    this.form.setValue({
      FullName: '',
      Department: ''
    });
  }
}
