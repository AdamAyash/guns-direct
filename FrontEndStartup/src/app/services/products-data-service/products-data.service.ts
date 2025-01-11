import { Injectable } from '@angular/core';
import { GetAllProductInputModel, GetAllProductOutputModel } from './models/get-all-products-models';
import { BaseServerRequestService } from '../../core/api/base-server-request-service';
import { IServiceResultProcessable } from '../../core/api/service-result-processable';
import { PageAnimationController } from '../../core/ui/pages/page-animation-controller/page-animation-controller';

@Injectable({
  providedIn: 'root'
})
export class ProductsDataService extends BaseServerRequestService {
  
   public getAllProducts( serviceProcessable: IServiceResultProcessable<GetAllProductOutputModel>,  pageAnimationController: PageAnimationController){
      let inputModel = new GetAllProductInputModel();

      this.sendServerRequest(
         "get_all_products",
         inputModel, 
         serviceProcessable,
         pageAnimationController
      );
   }

   public override getServiceDomain(): string {
      return "products/";
    }
}

