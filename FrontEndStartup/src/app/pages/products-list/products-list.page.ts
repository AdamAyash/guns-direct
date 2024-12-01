import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { ProductsDataService } from '../../services/products-data-service/products-data.service';
import { GetAllProductOutputModel } from '../../services/products-data-service/models/get-all-products-models';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [],
  templateUrl: './products-list.page.html',
  styleUrl: './products-list.page.css'
})
export class ProductsListPage extends BasePage<ProductsListPageModel> {

   getAllProductsServiceResultProcessable: IServiceResultProcessable<GetAllProductOutputModel> = {
      processResult: (prcessResult: GetAllProductOutputModel): boolean => {
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

}

