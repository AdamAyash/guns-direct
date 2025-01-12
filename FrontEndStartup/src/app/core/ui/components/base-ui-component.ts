import { Component, Directive, forwardRef, Input, OnInit } from '@angular/core';
import { BaseComponentInteractor } from '../interactors/base-component-intercator';
import { BaseComponent } from './base-component';
import { BaseUIComponentInteractor } from '../interactors/base-ui-component-interactor';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { InputControlPosition } from './base-ui-component-position';

@Directive({
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => BaseUIComponent),
            multi: true,
        },
    ],
})
export abstract class BaseUIComponent<Intercator extends BaseUIComponentInteractor> extends BaseComponent<Intercator>
    implements ControlValueAccessor {

  @Input() public position?: InputControlPosition = InputControlPosition.Column;
  @Input() public lableTextValue?: string = "";
  @Input() public placeholderValue: string = "";

    private onChange?: (value: string) => void;
    private onTouched?: () => void;

    protected _textValue: string = "";

    protected abstract transferDataToInteractor(): void;
    protected abstract transferDataToComponent(): void;

    writeValue(obj: any): void {
        this._textValue = obj;
    }
    registerOnChange(fn: (value: string) => void): void {
        this.onChange = fn;
    }

    public registerOnTouched(fn: () => void): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
    }

    protected onInput(event: Event): void {
        const input = event.target as HTMLInputElement;
        this._textValue = input.value;
        this.onChange!(this._textValue);
    }
}
