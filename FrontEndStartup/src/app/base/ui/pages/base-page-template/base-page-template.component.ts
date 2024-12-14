import { Component, Input } from '@angular/core';
import { BasePage } from '../base-page';
import { LoadingSpinnerComponent } from "../../../../components/loading-spinner/loading-spinner.component";
import { NavigatorMenuComponent } from "../../../../components/navigator-menu/navigator-menu.component";
import { NavigatorMenuIntercator } from '../../../../components/navigator-menu/interactors/navigator-menu.interactor';

@Component({
  selector: 'base-page-template',
  standalone: true,
  imports: [LoadingSpinnerComponent, NavigatorMenuComponent],
  templateUrl: './base-page-template.component.html',
  styleUrl: './base-page-template.component.css'
})
export class BasePageTemplateComponent {

  @Input() basePageReference!: BasePage<any>;

  public navigatorMenuComponentNavigator = new NavigatorMenuIntercator();

}
