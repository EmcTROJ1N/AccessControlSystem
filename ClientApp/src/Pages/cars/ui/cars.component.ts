import { Component, OnInit } from '@angular/core';
import { CarService } from 'Shared/Services/car';
import { Car } from 'Shared/Models/car';
import {FormsModule} from "@angular/forms";
import {CarDetailsComponent} from "Widgets/car-details";
import {ButtonComponent} from "Shared/Components/button";
import {ListComponent} from "Widgets/list";
import {CarListItemComponent} from "Entities/car-list-item";

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  standalone: true,
  imports: [
    FormsModule,
    CarDetailsComponent,
    ButtonComponent,
    ListComponent,
    CarListItemComponent
  ],
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {
  cars?: Car[];
  currentCar: Car = new Car();
  currentIndex = -1;
  licensePlate = '';

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    this.retrieveCars();
  }

  retrieveCars(): void {
    this.carService.getAll().subscribe({
      next: (data) => {
        this.cars = data.Result;
        console.log(data);
      },
      error: (e) => console.error(e)
    });
  }

  refreshList(): void {
    this.retrieveCars();
    this.currentCar = new Car();
    this.currentIndex = -1;
  }

  setActiveCar(car: Car, index: number): void {
    this.currentCar = car;
    this.currentIndex = index;
  }

  searchByLicensePlate(): void {
    this.currentCar = new Car();
    this.currentIndex = -1;

    this.carService.findByLicensePlate(this.licensePlate).subscribe({
      next: (data) => {
        this.cars = data;
        console.log(data);
      },
      error: (e) => console.error(e)
    });
  }
}
