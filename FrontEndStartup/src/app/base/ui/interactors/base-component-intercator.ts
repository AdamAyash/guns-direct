import { BaseComponent } from "../components/base-component";

export class BaseComponentInteractor{
    
    protected _control?: BaseComponent<this>;
    private _isEnabled: boolean = false;

    public get isEnabled(): boolean{
        return this._isEnabled;
    }

    public set isEnabled(v: boolean){
         this._isEnabled = v;
    }

    constructor() {

    }

    public setControl(control: BaseComponent<this>): void{
        this._control = control;
    }
}