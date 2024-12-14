import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { IServiceResultProcessable } from './service-result-processable';
import { BaseServerResponse } from './base-server-response';
import { BasePage } from '../ui/pages/base-page';
import { Guid } from "guid-typescript";
import { PageAnimationController } from '../ui/pages/page-animation-controller/page-animation-controller';

@Injectable()
export abstract class BaseService {
  constructor(private httpClient: HttpClient) {}

  public abstract getServiceDomain(): string;

  protected sendServerRequest<InputModel, OutputModel>(
    serviceRoute: string,
    inputModel: InputModel,
    serviceProcessable: IServiceResultProcessable<OutputModel>,
    pageAnimationController: PageAnimationController
  ) {

    let requestId: string = Guid.create().toString();
    pageAnimationController.registerAnimation(requestId);

    this.httpClient
      .post<BaseServerResponse<OutputModel>>(this.constructFullRequestURL(serviceRoute), inputModel)
      .subscribe((serverResponse) => {
        if(serverResponse.result && serverResponse.isSuccessful){
            serviceProcessable.processResult(serverResponse.result);
        }
        else{
          serviceProcessable.processError();
        }
        pageAnimationController.removeAnimation(requestId);

      });
  }

  private constructFullRequestURL(serviceRoute: string): string {
      return environment.serverUrl + this.getServiceDomain() + serviceRoute;
  }
}
