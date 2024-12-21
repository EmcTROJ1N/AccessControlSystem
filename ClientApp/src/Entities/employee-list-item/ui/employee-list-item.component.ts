import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Employee} from "@Models/employee";

@Component({
  selector: 'app-employee-list-item',
  templateUrl: './employee-list-item.component.html',
  standalone: true,
  styleUrl: './employee-list-item.component.css'
})
export class EmployeeListItemComponent {
  @Input() employee: Employee = null!;
  @Input() active: boolean = false;
  @Output() click: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();
  onClick = (e: MouseEvent) => this.click.emit(e);
}
