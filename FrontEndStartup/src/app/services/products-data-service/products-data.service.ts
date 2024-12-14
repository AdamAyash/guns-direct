import { Injectable } from '@angular/core';
import { BaseService } from '../../base/server/base-service';
import { GetAllProductInputModel, GetAllProductOutputModel } from './models/get-all-products-models';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';
import { BasePage } from '../../base/ui/pages/base-page';
import { PageAnimationController } from '../../base/ui/pages/page-animation-controller/page-animation-controller';

@Injectable({
  providedIn: 'root'
})
export class ProductsDataService extends BaseService {
  
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

