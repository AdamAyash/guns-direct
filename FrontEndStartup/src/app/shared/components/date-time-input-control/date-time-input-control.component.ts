import { Component, forwardRef } from '@angular/core';
import { BaseUIComponent } from '../../../core/ui/components/base-ui-component';
import { DateTimeInputControlInteractor } from './interactor/date-time-input-control.interactor';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { DateTimeFormatService } from '../../../services/date-time-format-service/date-time-format-service';

@Component({
  selector: 'date-time-input-control',
  standalone: true,
  imports: [
    CommonModule
    , ReactiveFormsModule
    , FormsModule
  ],
    providers: [
          {
              provide: NG_VALUE_ACCESSOR,
              useExisting: forwardRef(() => DateTimeInputControlComponent),
              multi: true,
          },
      ],
  templateUrl: './date-time-input-control.component.html',
  styleUrl: './date-time-input-control.component.css'
})
export class DateTimeInputControlComponent extends BaseUIComponent<DateTimeInputControlInteractor> {

  constructor(private dateTimeFormatService: DateTimeFormatService) {
    super();
  }

  protected override transferDataToInteractor(): void {
      this.interactor.dateTimeValue = this.dateTimeFormatService.getDateNewDateFromString(this._textValue);
  }
  protected override transferDataToComponent(): void {
  }
  
}
