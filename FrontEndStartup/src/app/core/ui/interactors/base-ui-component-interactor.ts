import { BaseComponentInteractor } from "./base-component-intercator";

export class BaseUIComponentInteractor extends BaseComponentInteractor{

    private readonly _uniqueElementId;

    constructor(uniqueElementId: string){
        super();
        this._uniqueElementId = uniqueElementId;
    }

    public getUniqueElementId(): string{
        return this._uniqueElementId;
    }
}