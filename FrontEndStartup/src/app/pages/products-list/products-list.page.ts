import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { ProductsDataService } from '../../services/products-data-service/products-data.service';
import { GetAllProductOutputModel } from '../../services/products-data-service/models/get-all-products-models';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';
import { ProductCardComponent } from "../../components/product-card/product-card.component";
import { ProductCardComponentInteractor } from '../../components/product-card/interactor/product-card.interactor';
import { Product } from '../../domain/products/products-model';
import { BasePageTemplateComponent } from "../../base/ui/pages/base-page-template/base-page-template.component";
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'products-list-page',
  standalone: true,
  imports: [ProductCardComponent, BasePageTemplateComponent, RouterModule, RouterOutlet],
  templateUrl: './products-list.page.html',
  styleUrl: './products-list.page.css'
})
export class ProductsListPage extends BasePage<ProductsListPageModel> {

  public _productsList?: Product[];

   getAllProductsServiceResultProcessable: IServiceResultProcessable<GetAllProductOutputModel> = {
      processResult: (resultData: GetAllProductOutputModel): boolean => {

          this._productsList = resultData.products;
          this.pageModel.productCardComponentInteractor.setProductData(resultData.products![0]);

          return true;
      },
      processError: () => {
          
      }
   }

  constructor(private productsDataService: ProductsDataService){
    super();
  }

  protected override loadData(): void {

    this.productsDataService.getAllProducts(this.getAllProductsServiceResultProcessable, this.pageAnimtaionController);
  }

  protected override initControls(): void {
  }

  protected override createNewPageModel(): ProductsListPageModel {
    return new ProductsListPageModel();
  }
}

class ProductsListPageModel{
    productCardComponentInteractor: ProductCardComponentInteractor;

    constructor() {
      this.productCardComponentInteractor = new ProductCardComponentInteractor();
    }
}

