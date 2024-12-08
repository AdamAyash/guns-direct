import { Directive, OnInit } from "@angular/core";

@Directive()
export abstract class BasePage<PageModel> implements OnInit {

    private _pageModel!: PageModel;

    protected get pageModel(): PageModel{
        return this._pageModel;
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