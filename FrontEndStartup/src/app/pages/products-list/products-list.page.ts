import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { ProductsDataService } from '../../services/products-data-service/products-data.service';
import { GetAllProductOutputModel } from '../../services/products-data-service/models/get-all-products-models';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';
import { ProductCardComponent } from "../../components/product-card/product-card.component";
import { ProductCardComponentInteractor } from '../../components/product-card/interactor/product-card.interactor';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './products-list.page.html',
  styleUrl: './products-list.page.css'
})
export class ProductsListPage extends BasePage<ProductsListPageModel> {

   getAllProductsServiceResultProcessable: IServiceResultProcessable<GetAllProductOutputModel> = {
      processResult: (resultData: GetAllProductOutputModel): boolean => {

          //resultData.products![0].imageURL = "https://zarimex.eu/image/cache/catalog/data/Armsan%20/612_softtouchN-280x280w.jpg";
          let image = resultData.products![0];
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

    this.productsDataService.getAllProducts(this.getAllProductsServiceResultProcessable);
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

