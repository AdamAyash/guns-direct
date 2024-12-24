import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { NavigatorMenuComponent } from "../../components/navigator-menu/navigator-menu.component";
import { NavigatorMenuIntercator } from '../../components/navigator-menu/interactors/navigator-menu.interactor';
import { ProductCardComponent } from "../../components/product-card/product-card.component";
import { RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';
import { BasePageTemplateComponent } from "../../base/ui/pages/base-page-template/base-page-template.component";
import { ProductsDataService } from '../../services/products-data-service/products-data.service';
import { Product } from '../../domain/products/products-model';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';
import { GetAllProductOutputModel } from '../../services/products-data-service/models/get-all-products-models';
import { ProductCardComponentInteractor } from '../../components/product-card/interactor/product-card.interactor';
import { log } from 'console';

@Component({
  selector: 'home-page',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    BasePageTemplateComponent,
    ProductCardComponent,
],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css'
})
export class HomePage extends BasePage<HomePageModel> {

  public _productsList?: Product[];
  public _productCarouselItemsArray: Array<Product[]> = new  Array<Product[]>();

  getAllProductsServiceResultProcessable: IServiceResultProcessable<GetAllProductOutputModel> = {
    processResult: (resultData: GetAllProductOutputModel): boolean => {

      if(resultData.products)
          this._productsList = resultData.products;

        this.pageModel.productCardComponentInteractor.setProductData(resultData.products![0]);
        this.processCarouselData();

        return true;
    },
    processError: () => {
        
    }
 }

 private processCarouselData(){
  if(!this._productsList)
      return;

    let maxIterations = this._productsList.length / 7;
    for(let i = 0; i < maxIterations; i++){
        this._productCarouselItemsArray.push(this._productsList.slice(0, 7));
    }
    console.log(this._productCarouselItemsArray);
 }

  constructor(private productsDataService: ProductsDataService){
    super();
  }

  protected override loadData(): void {
      this.productsDataService.getAllProducts(this.getAllProductsServiceResultProcessable, 
        this.pageAnimtaionController);
  }

  protected override initControls(): void {
  }

  protected override createNewPageModel(): HomePageModel {
    return new HomePageModel();
  }
  
}

class HomePageModel{

    productCardComponentInteractor: ProductCardComponentInteractor;

    constructor() {
      this.productCardComponentInteractor = new ProductCardComponentInteractor();
    }
}
