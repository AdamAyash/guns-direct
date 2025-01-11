import { BaseUIComponentInteractor } from "../../../../core/ui/interactors/base-ui-component-interactor";
import { TextInputControlComponent } from "../text-input.component";

export class TextInputComponentInteractor extends BaseUIComponentInteractor {

   private _textValue : string = "";

   public get textValue() : string {
    return this._textValue;
   }
   public set textValue(v : string) {
    this._textValue = v;
   }
    
   public isNullOrEmpty(): boolean{
      return this.textValue == "" || this._textValue == undefined;
   }

}