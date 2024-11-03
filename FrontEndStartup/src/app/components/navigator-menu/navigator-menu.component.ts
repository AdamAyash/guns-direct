import { Component } from '@angular/core';
import { BaseComponent } from '../../base/ui/components/base-component';
import { NavigatorMenuIntercator } from './interactors/navigator-menu.interactor';

@Component({
  selector: 'navigator-menu',
  standalone: true,
  imports: [],
  templateUrl: './navigator-menu.component.html',
  styleUrl: './navigator-menu.component.css'
})
export class NavigatorMenuComponent extends BaseComponent<NavigatorMenuIntercator>{

   constructor(){
      super();
   }

}
