import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { IServiceResultProcessable } from './service-result-processable';
import { BaseServerResponse } from './base-server-response';
import { BasePage } from '../ui/pages/base-page';
import { Guid } from 'guid-typescript';
import { PageAnimationController } from '../ui/pages/page-animation-controller/page-animation-controller';
import { catchError, retry, throwError } from 'rxjs';

@Injectable()
export abstract class BaseServerRequestService {

  private readonly _maxRequestRetryCount = 0;
  private readonly _stopAnimationTimeout = 5000;

  constructor(private httpClient: HttpClient) { }

  protected abstract getServiceDomain(): string;

  protected sendServerRequest<InputModel, OutputModel>(
    serviceRoute: string,
    inputModel: InputModel,
    serviceProcessable: IServiceResultProcessable<OutputModel>,
    pageAnimationController: PageAnimationController
  ) {
    let requestId: string = Guid.create().toString();
    pageAnimationController.registerAnimation(requestId);

    this.httpClient
      .post<BaseServerResponse<OutputModel>>(
        this.constructFullRequestURL(serviceRoute),
        inputModel
      )
      .pipe(retry(this._maxRequestRetryCount), catchError(this.handleError))
      .subscribe((serverResponse) => {
        if (serverResponse.result && serverResponse.isSuccessful) {
          if (!serviceProcessable.processResult(serverResponse.result)) {
          }
        } else {
          serviceProcessable.processError();
          pageAnimationController.stopAnimation(requestId);
        }
        pageAnimationController.stopAnimation(requestId);
      });

    setTimeout(() => {
      pageAnimationController.stopAnimation(requestId);
    }, this._stopAnimationTimeout)
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
    } else {
      console.error(
        `Backend returned code ${error.status}, body was: `,
        error.error
      );
    }
    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  }

  private constructFullRequestURL(serviceRoute: string): string {
    return environment.serverUrl + this.getServiceDomain() + serviceRoute;
  }
}
