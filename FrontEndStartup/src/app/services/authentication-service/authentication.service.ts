import { Injectable } from '@angular/core';
import { LoginInputModel, LoginOutputModel, RegisterInputModel, RegisterOutputModel } from './models/authentication-models';
import { BaseServerRequestService } from '../../core/api/base-server-request-service';
import { IServiceResultProcessable } from '../../core/api/service-result-processable';
import { PageAnimationController } from '../../core/ui/pages/page-animation-controller/page-animation-controller';
import { HttpClient } from '@angular/common/http';
import { JwtService } from '../jwt-service/jwt-service';
import { Router } from '@angular/router';

@Injectable({
   providedIn: 'root'
})
export class AuthenticationService extends BaseServerRequestService {

   constructor(httpClient: HttpClient
      , private jwtService: JwtService
      , private router: Router) {
      super(httpClient);
   }

   public login(inputModel: LoginInputModel, serviceProcessable: IServiceResultProcessable<LoginOutputModel>, pageAnimationController: PageAnimationController) {

      this.sendServerRequest(
         "login",
         inputModel,
         serviceProcessable,
         pageAnimationController
      );
   }

   public register(inputModel: RegisterInputModel, serviceProcessable: IServiceResultProcessable<RegisterOutputModel>, pageAnimationController: PageAnimationController) {
      this.sendServerRequest(
         "register",
         inputModel,
         serviceProcessable,
         pageAnimationController
      );
   }

   protected override getServiceDomain(): string {
      return "authentication/";
   }

   public logout() {
      this.jwtService.clear();
      this.router.navigateByUrl('/login');
   }

   public isUserAuthenticated(): boolean {
      return this.jwtService.isValid()
   }
}
