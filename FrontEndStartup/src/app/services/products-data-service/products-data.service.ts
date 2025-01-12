import { Injectable } from '@angular/core';
import { GetAllProductInputModel, GetAllProductOutputModel } from './models/get-all-products-models';
import { BaseServerRequestService } from '../../core/api/base-server-request-service';
import { IServiceResultProcessable } from '../../core/api/service-result-processable';
import { PageAnimationController } from '../../core/ui/pages/page-animation-controller/page-animation-controller';
import { GetProductByIdInputModel, GetProductByIdOutputtModel } from './models/get-product-by-id-models';

@Injectable({
   providedIn: 'root'
})
export class ProductsDataService extends BaseServerRequestService {

   public getAllProducts(serviceProcessable: IServiceResultProcessable<GetAllProductOutputModel>, pageAnimationController: PageAnimationController) {
      let inputModel = new GetAllProductInputModel();

      this.sendServerRequest(
         "get_all_products",
         inputModel,
         serviceProcessable,
         pageAnimationController
      );
   }

   public getProductById(productId: string,
      serviceProcessable: IServiceResultProcessable<GetProductByIdOutputtModel>, pageAnimationController: PageAnimationController) {

      let inputModel = new GetProductByIdInputModel();
      inputModel.productId = productId;

      this.sendServerRequest(
         "get_product_by_id",
         inputModel,
         serviceProcessable,
         pageAnimationController
      );
   }

   public override getServiceDomain(): string {
      return "products/";
   }
}

