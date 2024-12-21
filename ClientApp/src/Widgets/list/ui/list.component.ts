import {Component, Input} from '@angular/core';
import {CarListItemComponent} from "Entities/car-list-item";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  standalone: true,
  imports: [
    CarListItemComponent
  ],
  styleUrl: './list.component.css'
})
export class ListComponent {
}
