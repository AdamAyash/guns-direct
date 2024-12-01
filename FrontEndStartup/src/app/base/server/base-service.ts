import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { IServiceResultProcessable } from './service-result-processable';

@Injectable()
export abstract class BaseService {
  constructor(private httpClient: HttpClient) {}

  public abstract getServiceDomain(): string;

  protected sendServerRequest<InputModel, OutputModel>(
    serviceRoute: string,
    inputModel: InputModel,
    serviceProcessable: IServiceResultProcessable<OutputModel>
  ) {
    this.httpClient
      .post<OutputModel>(this.constructFullRequestURL(serviceRoute), inputModel)
      .subscribe((result) => {
        serviceProcessable.processResult(result);
      });
  }

  private constructFullRequestURL(serviceRoute: string): string {
    return environment.serverUrl + this.getServiceDomain() + serviceRoute;
  }
}
