import { Component, forwardRef, Input } from '@angular/core';
import { BaseUIComponent } from '../../../core/ui/components/base-ui-component';
import { PasswordInputComponentInteractor } from './interactor/password-input.component.interactor';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { InputControlPosition } from '../../../core/ui/components/base-ui-component-position';

@Component({
  selector: 'password-input-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './password-input.component.html',
  styleUrl: './password-input.component.css',
   providers: [
          {
              provide: NG_VALUE_ACCESSOR,
              useExisting: forwardRef(() => PasswordInputControlComponent),
              multi: true,
          },
      ],
})
export class PasswordInputControlComponent extends BaseUIComponent<PasswordInputComponentInteractor> {

  protected override transferDataToInteractor(): void {
    this.interactor.passwordValue = this._textValue;
  }
  protected override transferDataToComponent(): void {
  }
}
