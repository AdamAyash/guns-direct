import { Injectable } from '@angular/core';
import { BaseService } from '../../base/base-service/base-service';

@Injectable({
  providedIn: 'root'
})
export class ProductsDataService extends BaseService {
  
   public getAllProducts(): void{
   }

   public override getServiceDomain(): string {
    throw new Error('Method not implemented.');
  }
}
