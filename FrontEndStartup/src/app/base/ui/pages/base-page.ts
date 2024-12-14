import { Directive, OnInit } from "@angular/core";
import { PageAnimationController } from "./page-animation-controller/page-animation-controller";

@Directive()
export abstract class BasePage<PageModel> implements OnInit {

    private _pageModel!: PageModel;
    private _pageAnimtaionController: PageAnimationController = new PageAnimationController();

    protected get pageModel(): PageModel{
        return this._pageModel;
    }

    public get pageAnimtaionController(){
      return this._pageAnimtaionController;
    }

    constructor(){
    }

    ngOnInit(): void {
        this._pageModel = this.createNewPageModel();
        this.initControls();
        this.loadData();

    }

    protected abstract loadData(): void;
    protected abstract initControls(): void;
    protected abstract createNewPageModel(): PageModel; 
}