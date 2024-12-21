import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Car} from "Shared/Models/car";

@Component({
  selector: 'app-car-list-item',
  templateUrl: './car-list-item.component.html',
  standalone: true,
  styleUrl: './car-list-item.component.css'
})
export class CarListItemComponent {
  @Input() car: Car = null!;
  @Input() active: boolean = false;
  @Output() click: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();
  onClick = (e: MouseEvent) => this.click.emit(e);
}
