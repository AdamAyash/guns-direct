import { BaseUIComponentInteractor } from "../../../../core/ui/interactors/base-ui-component-interactor";

export class PasswordInputComponentInteractor extends BaseUIComponentInteractor{
    
    private _passwordValue : string = "";

    public get passwordValue() : string {
        return this._passwordValue;
    }
    
    public set passwordValue(v : string) {
        this._passwordValue = v;
    }
    
}