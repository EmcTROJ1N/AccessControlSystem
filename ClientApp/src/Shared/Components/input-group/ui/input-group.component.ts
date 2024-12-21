import {
  Component,
  Input,
  forwardRef,
  Provider
} from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
  NG_VALIDATORS,
  Validator,
  AbstractControl,
  ValidationErrors, FormsModule
} from '@angular/forms';

@Component({
  selector: 'app-input-group',
  templateUrl: './input-group.component.html',
  standalone: true,
  imports: [FormsModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputGroupComponent),
      multi: true,
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => InputGroupComponent),
      multi: true,
    },
  ],
  styleUrls: ['./input-group.component.css'],
})
export class InputGroupComponent implements ControlValueAccessor, Validator {
  @Input() label: string = '';
  @Input() name: string = '';
  value: string = '';


  onInputChange(event: any): void {
    this.value = event.target.value; // Сохраняем новое значение
    this.onChange(this.value); // Сообщаем родительскому компоненту о изменении значения
    this.onTouched(); // Сообщаем, что элемент был затронут
  }


  private onChange: (value: string) => void = () => {
  };
  private onTouched: () => void = () => {};

  // ControlValueAccessor methods
  writeValue(value: string): void {
    this.value = value || '';
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // Optional: Add logic if the input needs to handle disabled state.
  }

  handleInput(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    this.value = inputElement.value;
    this.onChange(this.value);
  }

  handleBlur(): void {
    this.onTouched();
  }

  // Validator methods
  validate(control: AbstractControl): ValidationErrors | null {
    return this.value ? null : { required: true };
  }
}
