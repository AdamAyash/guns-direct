import { Injectable } from '@angular/core';
import { BaseService } from '../../base/server/base-service';
import { GetAllProductInputModel, GetAllProductOutputModel } from './models/get-all-products-models';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';

@Injectable({
  providedIn: 'root'
})
export class ProductsDataService extends BaseService {
  
   public getAllProducts( serviceProcessable: IServiceResultProcessable<GetAllProductOutputModel>){
      let inputModel = new GetAllProductInputModel();

      this.sendServerRequest(
         "get_all_products",
         inputModel, 
         serviceProcessable
      );
   }

   public override getServiceDomain(): string {
      return "products/";
    }
}

