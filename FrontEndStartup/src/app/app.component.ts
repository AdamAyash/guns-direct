import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigatorMenuComponent } from './components/navigator-menu/navigator-menu.component';
import { NavigatorMenuIntercator } from './components/navigator-menu/interactors/navigator-menu.interactor';
import { ProductsDataService } from './services/products-data-service/products-data.service';
import { ProductCardComponent } from "./components/product-card/product-card.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavigatorMenuComponent, ProductCardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Guns Direct';

  interactor: NavigatorMenuIntercator;

constructor(private productsDataService: ProductsDataService){
  this.interactor = new NavigatorMenuIntercator();
  this.productsDataService.getAllProducts();
}

}
