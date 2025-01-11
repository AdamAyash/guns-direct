import { Component } from '@angular/core';
import { NavigatorMenuIntercator } from './interactors/navigator-menu.interactor';
import { RouterModule} from '@angular/router';
import { SearchBarComponent } from "../search-bar/search-bar.component";
import { BaseComponent } from '../../../core/ui/components/base-component';
import { AuthenticationService } from '../../../services/authentication-service/authentication.service';

@Component({
  selector: 'navigator-menu',
  standalone: true,
  imports: [RouterModule, SearchBarComponent],
  templateUrl: './navigator-menu.component.html',
  styleUrl: './navigator-menu.component.css'
})
export class NavigatorMenuComponent extends BaseComponent<NavigatorMenuIntercator>{

   constructor(private authenticationService: AuthenticationService){
      super();
   }

   public isUserAuthenticated(): boolean{
      return this.authenticationService.isUserAuthenticated();
   }

   public logoutUser(): void{
      this.authenticationService.logout();
   }
}
