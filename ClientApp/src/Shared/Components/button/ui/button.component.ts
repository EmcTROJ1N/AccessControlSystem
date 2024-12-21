import {Component, EventEmitter, Input, Output, signal} from '@angular/core';
import {ButtonColor, ButtonSize, ButtonType} from "../model";

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  standalone: true,
  styleUrl: './button.component.css'
})
export class ButtonComponent {
  @Input() text: string = 'Button';

  @Input() color: ButtonColor = 'primary';
  @Input() type: ButtonType = 'regular';
  @Input() size: ButtonSize = 'sm';

  @Input() href: string = '#';
  @Output() click: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();

  onClick(e: MouseEvent) {
    if (this.href == '#')
      e.preventDefault();
    //this.click.emit(e);
  }

  get btnClasses(): string {
    const classes = ['btn'];

    if (this.type !== 'block') {
      if (this.type !== 'regular') classes.push(`btn-${this.type}`);
      classes.push(`btn-${this.color}`);
      if (this.size) classes.push(`btn-${this.size}`);
    } else {
      classes.push('btn-block', `btn-${this.color}`);
    }

    return classes.join(' ');
  }
}
