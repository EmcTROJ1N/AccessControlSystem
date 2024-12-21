import { Component } from '@angular/core';
import {ButtonComponent} from "Shared/Components/button";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputGroupComponent} from "Shared/Components/input-group";
import {AccessControllerService} from "@Services/access-controller";

@Component({
  selector: 'app-access-check',
  templateUrl: './access-check.component.html',
  standalone: true,
  imports: [
    ButtonComponent,
    FormsModule,
    InputGroupComponent,
    ReactiveFormsModule
  ],
  styleUrl: './access-check.component.css'
})
export class AccessCheckComponent {
  submitted = false;
  form: FormGroup;
  message: string = '';

  constructor(private accessCheck: AccessControllerService,
              private fb: FormBuilder) {
    this.form = this.fb.group({
      LicensePlate: ['', Validators.required],
      EmployeeId: ['', Validators.required]
    })
  }

  checkAccess(): void {
    const data = this.form.value;
    this.accessCheck.check(data.LicensePlate, data.EmployeeId).subscribe({
      next: (res) => {
        console.log(res);
        this.message = 'Access granted';
        this.submitted = true;
      },
      error: (e) => {
        console.error(e)
        this.message = 'Access denied';
        this.submitted = true;
      }
    });
  }

  newCheck(): void {
    this.submitted = false;
    this.form.setValue({
      LicensePlate: '',
      EmployeeId: ''
    });
  }
}
