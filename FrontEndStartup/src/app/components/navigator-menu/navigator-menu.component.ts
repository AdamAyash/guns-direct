import { Component } from '@angular/core';
import { BaseComponent } from '../../base/ui/components/base-component';
import { NavigatorMenuIntercator } from './interactors/navigator-menu.interactor';
import { RouterModule} from '@angular/router';

@Component({
  selector: 'navigator-menu',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navigator-menu.component.html',
  styleUrl: './navigator-menu.component.css'
})
export class NavigatorMenuComponent extends BaseComponent<NavigatorMenuIntercator>{

   constructor(){
      super();
   }

}
