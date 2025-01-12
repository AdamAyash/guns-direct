export class PageAnimationController{

    private _registeredPageAnimationsArray: Array<string> = new Array();
   
    public get isAnimationActive(){
        return this._registeredPageAnimationsArray.length > 0;
    }

    public registerAnimation(uuid: string){
        this._registeredPageAnimationsArray.push(uuid);
    }

    public stopAnimation(uuid: string){
       let animationIndex = this._registeredPageAnimationsArray.indexOf(uuid, 0);
       if(animationIndex >= 0){
            this._registeredPageAnimationsArray.splice(animationIndex, 1);
       }
    }

}