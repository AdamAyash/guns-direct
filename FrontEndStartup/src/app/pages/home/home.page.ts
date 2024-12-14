import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { NavigatorMenuComponent } from "../../components/navigator-menu/navigator-menu.component";
import { NavigatorMenuIntercator } from '../../components/navigator-menu/interactors/navigator-menu.interactor';
import { ProductCardComponent } from "../../components/product-card/product-card.component";
import { RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';
import { BasePageTemplateComponent } from "../../base/ui/pages/base-page-template/base-page-template.component";

@Component({
  selector: 'home-page',
  standalone: true,
  imports: [RouterOutlet, RouterModule, BasePageTemplateComponent],
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

  constructor(){

  }
}
