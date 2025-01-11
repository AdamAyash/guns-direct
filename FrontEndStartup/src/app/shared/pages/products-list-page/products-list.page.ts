import { Component, Injector } from '@angular/core';
import { ProductCardComponent } from "../../components/product-card/product-card.component";
import { ProductCardComponentInteractor } from '../../components/product-card/interactor/product-card.interactor';
import { Product } from '../../domain/products/products-model';
import { RouterModule, RouterOutlet } from '@angular/router';
import { BasePageTemplateComponent } from '../../../core/ui/pages/base-page-template/base-page-template.component';
import { BasePage } from '../../../core/ui/pages/base-page';
import { IServiceResultProcessable } from '../../../core/api/service-result-processable';
import { GetAllProductOutputModel } from '../../../services/products-data-service/models/get-all-products-models';
import { ProductsDataService } from '../../../services/products-data-service/products-data.service';

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

  constructor(injector: Injector
   , private productsDataService: ProductsDataService){
    super(injector);
  }

  protected override loadData(): void {

    this.productsDataService.getAllProducts(this.getAllProductsServiceResultProcessable, this.pageAnimtaionController);
  }

  protected override initControls(): void {
  }

  protected override createNewPageModel(): ProductsListPageModel {
    return new ProductsListPageModel();
  }

   protected override transferControlsToData(): void {
  }

  protected override validateData(): boolean {
      return true;
  }

   protected override onSubmitProcessable(): void {
    
  }
}

class ProductsListPageModel{
    productCardComponentInteractor: ProductCardComponentInteractor;

    constructor() {
      this.productCardComponentInteractor = new ProductCardComponentInteractor();
    }
}

