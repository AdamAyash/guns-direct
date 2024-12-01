import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { NavigatorMenuComponent } from "../../components/navigator-menu/navigator-menu.component";
import { NavigatorMenuIntercator } from '../../components/navigator-menu/interactors/navigator-menu.interactor';
import { ProductCardComponent } from "../../components/product-card/product-card.component";
import { RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'home-page',
  standalone: true,
  imports: [NavigatorMenuComponent, ProductCardComponent, RouterOutlet, RouterModule],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css'
})
export class HomePage extends BasePage<HomePageModel> {

  protected override loadData(): void {

  }

  protected override initControls(): void {
  }

  protected override createNewPageModel(): HomePageModel {
    return new HomePageModel();
  }
  
}

class HomePageModel{

  navigatorMenuInteractor: NavigatorMenuIntercator = new NavigatorMenuIntercator();

  constructor(){

  }
}
