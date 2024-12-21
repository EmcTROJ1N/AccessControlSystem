import { Component } from '@angular/core';
import {CarService} from "@Services/car";
import {Car} from "@Models/car";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputGroupComponent} from "../../../Shared/Components/input-group";
import {ButtonComponent} from "../../../Shared/Components/button";

@Component({
  selector: 'app-car-add',
  templateUrl: './car-add.component.html',
  standalone: true,
  imports: [
    FormsModule,
    InputGroupComponent,
    ReactiveFormsModule,
    ButtonComponent
  ],
  styleUrl: './car-add.component.css'
})
export class CarAddComponent {
  submitted = false;
  form: FormGroup;

  constructor(private carService: CarService,
              private fb: FormBuilder) {
    this.form = this.fb.group({
      LicensePlate: ['', Validators.required],
      EmployeeId: ['', Validators.required]
    })
  }

  saveCar(): void {
    this.carService.create(this.form.value).subscribe({
      next: (res) => {
        console.log(res);
        this.submitted = true;
      },
      error: (e) => console.error(e)
    });
  }

  newCar(): void {
    this.submitted = false;
    this.form.setValue({
      LicensePlate: '',
      EmployeeId: ''
    });
  }
}
