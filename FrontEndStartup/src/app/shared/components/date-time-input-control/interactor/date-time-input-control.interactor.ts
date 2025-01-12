import { BaseUIComponentInteractor } from "../../../../core/ui/interactors/base-ui-component-interactor";

export class DateTimeInputControlInteractor extends BaseUIComponentInteractor{

    private _dateTimeValue : Date = new Date();

    public get dateTimeValue() : Date {
        return this._dateTimeValue;
    }
    public set dateTimeValue(v : Date) {
        this._dateTimeValue = v;
    }
}