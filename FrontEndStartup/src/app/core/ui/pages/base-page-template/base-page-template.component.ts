import { Component, Input, Output } from '@angular/core';
import { BasePage } from '../base-page';
import { CommonModule } from '@angular/common';
import { NavigatorMenuIntercator } from '../../../../shared/components/navigator-menu/interactors/navigator-menu.interactor';
import { LoadingSpinnerComponent } from "../../../../shared/components/loading-spinner/loading-spinner.component";
import { NavigatorMenuComponent } from "../../../../shared/components/navigator-menu/navigator-menu.component";
import { FooterComponent } from "../../../../shared/components/footer/footer.component";
import { ToastModule } from 'primeng/toast';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

export enum BasePageContentBackgroundColor{
  White = "white",
  Default = "default"
}

@Component({
  selector: 'base-page-template',
  standalone: true,
  imports: [
    CommonModule,
    LoadingSpinnerComponent,
    NavigatorMenuComponent,
    FooterComponent,
    ToastModule,
],
  templateUrl: './base-page-template.component.html',
  styleUrl: './base-page-template.component.css'
})
export class BasePageTemplateComponent {

  @Input() public includeNavigation: boolean  = true;
  @Input() public basePageReference!: BasePage<any>;
  @Input() public basePageContentBackgroundColor: BasePageContentBackgroundColor = BasePageContentBackgroundColor.Default;

  public navigatorMenuComponentNavigator = new NavigatorMenuIntercator();

}
