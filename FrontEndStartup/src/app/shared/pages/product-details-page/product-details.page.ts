import { Component, Injector } from '@angular/core';
import { BasePageTemplateComponent } from '../../../core/ui/pages/base-page-template/base-page-template.component';
import { BasePage } from '../../../core/ui/pages/base-page';
import { ProductsDataService } from '../../../services/products-data-service/products-data.service';
import { IServiceResultProcessable } from '../../../core/api/service-result-processable';
import { GetProductByIdOutputtModel } from '../../../services/products-data-service/models/get-product-by-id-models';

@Component({
  selector: 'product-details-page',
  standalone: true,
  imports: [BasePageTemplateComponent],
  templateUrl: './product-details.page.html',
  styleUrl: './product-details.page.css'
})
export class ProductDetailsPage extends BasePage<ProductDetailsPageModel> {

  getProductByIdServiceResultProcessable: IServiceResultProcessable<GetProductByIdOutputtModel> = {
    processResult: (resultData: GetProductByIdOutputtModel): boolean => {

      return true;
    },
    processError: () => {

    }
  }

  constructor(injector: Injector,
    private productDataService: ProductsDataService) {
    super(injector)
  }

  protected override initControls(): void {
  }

  protected override loadData(): void {

    let productId = this.route.snapshot.params['id'];
    if (productId <= 0) {
      return;
    }

    this.productDataService.getProductById(productId,
      this.getProductByIdServiceResultProcessable, this.pageAnimtaionController);

  }

  protected override createNewPageModel(): ProductDetailsPageModel {
    return new ProductDetailsPageModel();
  }

  protected override transferControlsToData(): void {
  }

  protected override validateData(): boolean {
    return true;
  }

  protected override onSubmitProcessable(): void {

  }

}

class ProductDetailsPageModel {

}
