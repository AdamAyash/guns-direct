import { Directive, inject, Injector, OnInit } from "@angular/core";
import { PageAnimationController } from "./page-animation-controller/page-animation-controller";
import { BasePageContentBackgroundColor } from "./base-page-template/base-page-template.component";
import { ActivatedRoute, Route } from "@angular/router";
import { Observable } from "rxjs";
import { FormBuilder, FormGroup } from "@angular/forms";
import { MessageService, PrimeNGConfig } from "primeng/api";
import { ppid } from "process";
import { Router } from '@angular/router';
import { TextInputControlType } from "../../../shared/components/text-input/text-input.component";
import { InputControlPosition } from "../components/base-ui-component-position";

export enum ToastMessageSeverity {
    Success = "success",
    Warn = "warn",
    Error = "error"
}

@Directive()
export abstract class BasePage<PageModel> implements OnInit {

    private readonly _toastMessageDelayInMiliseconds: number = 100;

    protected readonly formBuilder: FormBuilder;
    protected readonly messageService: MessageService;
    protected readonly primengConfig: PrimeNGConfig
    protected readonly router: Router;
    protected readonly route: ActivatedRoute = inject(ActivatedRoute);

    protected basePageFormGroup: any;

    private _pageModel!: PageModel;
    private _pageAnimtaionController: PageAnimationController = new PageAnimationController();

    public textInputControlType = TextInputControlType;
    public textInputPosition = InputControlPosition;
    public templateBackgroundColor = BasePageContentBackgroundColor;



    protected get pageModel(): PageModel {
        return this._pageModel;
    }

    public get pageAnimtaionController() {
        return this._pageAnimtaionController;
    }

    constructor(private injector: Injector,) {
        this.formBuilder = injector.get(FormBuilder);
        this.messageService = injector.get(MessageService);
        this.primengConfig = injector.get(PrimeNGConfig);
        this.router = injector.get(Router);
    }

    ngOnInit(): void {
        this._pageModel = this.createNewPageModel();
        this.primengConfig.ripple = true;
        this.initControls();
        this.loadData();

    }

    protected showToastMessage(severity: ToastMessageSeverity, title: string, messageContent: string): void {
        setTimeout(() => {
            this.messageService.add({
                severity: severity,
                summary: title,
                detail: messageContent
            });
        }, this._toastMessageDelayInMiliseconds);
    }

    protected abstract loadData(): void;
    protected abstract initControls(): void;
    protected abstract createNewPageModel(): PageModel;
    protected abstract validateData(): boolean;
    protected abstract transferControlsToData(): void;
    protected abstract onSubmitProcessable(): void;

    public onSubmit() {
        if (!this.validateData())
            return;

        this.onSubmitProcessable();
    }


}