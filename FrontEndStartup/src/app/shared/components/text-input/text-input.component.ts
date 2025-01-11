import { Component, forwardRef, Input } from '@angular/core';
import { BaseComponent } from '../../../core/ui/components/base-component';
import { TextInputComponentInteractor } from './interactor/text-input.component.interactor';
import { CommonModule } from '@angular/common';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { BaseUIComponent } from '../../../core/ui/components/base-ui-component';
import { InputControlPosition } from '../../../core/ui/components/base-ui-component-position';

export enum TextInputControlType{
  Text = "text",
  Email = "email",
  Phone = "phone"
}

@Component({
  selector: 'text-input-control',
  standalone: true,
  imports: [
    CommonModule
    , FormsModule
    , ReactiveFormsModule
  ],
   providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => TextInputControlComponent),
            multi: true,
        },
    ],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.css'
})
export class TextInputControlComponent extends BaseUIComponent<TextInputComponentInteractor> {

  @Input() public inputType: TextInputControlType = TextInputControlType.Text;

  transferDataToInteractor(): void {
    this.interactor.textValue = this._textValue;
  }

  transferDataToComponent(): void {
  }
  
}
