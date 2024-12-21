import {Component, Input, OnInit} from '@angular/core';
import {Car} from "@Models/car";
import {CarService} from "@Services/car";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {ButtonComponent} from "../../../Shared/Components/button";

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  standalone: true,
  imports: [
    FormsModule,
    RouterLink,
    ButtonComponent
  ],
  styleUrl: './car-details.component.css'
})
export class CarDetailsComponent implements OnInit {
  @Input() viewMode = false;
  @Input() currentCar: Car = new Car();

  message = '';

  constructor(
    private carService: CarService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      this.getCar(this.route.snapshot.params['id']);
    }
  }

  getCar(id: string): void {
    this.carService.get(id).subscribe({
      next: (data) => {
        this.currentCar = data;
        console.log(data);
      },
      error: (e) => console.error(e)
    });
  }

  updateAvailability(status: boolean): void {
    if (this.currentCar.Id == null)
      return;
    this.message = '';

    this.carService.update(this.currentCar.Id, this.currentCar).subscribe({
      next: (res) => {
        console.log(res);
        this.message = 'The availability status was updated successfully!';
      },
      error: (e) => console.error(e)
    });
  }

  updateCar(): void {
    if (this.currentCar.Id == null)
      return;
    this.message = '';

    this.carService.update(this.currentCar.Id, this.currentCar).subscribe({
      next: (res) => {
        console.log(res);
        this.message = 'The car was updated successfully!';
      },
      error: (e) => console.error(e)
    });
  }

  deleteCar(): void {
    if (this.currentCar.Id == null)
      return;
    this.carService.delete(this.currentCar.Id).subscribe({
      next: (res) => {
        console.log(res);
        this.router.navigate(['/cars']);
      },
      error: (e) => console.error(e)
    });
  }
}
