import { Injectable } from '@angular/core';
import { BaseService } from '../../base/server/base-service';
import { LoginInputModel, LoginOutputModel } from './models/authentication-models';
import { PageAnimationController } from '../../base/ui/pages/page-animation-controller/page-animation-controller';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends BaseService {
  
       public login( inputModel: LoginInputModel,  serviceProcessable: IServiceResultProcessable<LoginOutputModel>,  pageAnimationController: PageAnimationController){

          this.sendServerRequest(
             "login",
             inputModel, 
             serviceProcessable,
             pageAnimationController
          );
       }


    public override getServiceDomain(): string {
      return "authentication/";
    }
}
