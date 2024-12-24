import { Component, Input, Output } from '@angular/core';
import { BasePage } from '../base-page';
import { LoadingSpinnerComponent } from "../../../../components/loading-spinner/loading-spinner.component";
import { NavigatorMenuComponent } from "../../../../components/navigator-menu/navigator-menu.component";
import { NavigatorMenuIntercator } from '../../../../components/navigator-menu/interactors/navigator-menu.interactor';
import { CommonModule } from '@angular/common';
import { FooterComponent } from "../../../../components/footer/footer.component";

export enum BasePageContentBackgroundColor{
  White = "white",
  Default = "default"
}

@Component({
  selector: 'base-page-template',
  standalone: true,
  imports: [LoadingSpinnerComponent, NavigatorMenuComponent, CommonModule, FooterComponent],
  templateUrl: './base-page-template.component.html',
  styleUrl: './base-page-template.component.css'
})
export class BasePageTemplateComponent {

  @Input() public basePageReference!: BasePage<any>;
  @Input() public basePageContentBackgroundColor: BasePageContentBackgroundColor = BasePageContentBackgroundColor.Default;

  public navigatorMenuComponentNavigator = new NavigatorMenuIntercator();

}
