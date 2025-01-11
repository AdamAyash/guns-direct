import { Component, Injector } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoadingSpinnerComponent } from "../../components/loading-spinner/loading-spinner.component";
import { AuthenticationService } from '../../../services/authentication-service/authentication.service';
import { LoginInputModel, LoginOutputModel } from '../../../services/authentication-service/models/authentication-models';
import { IServiceResultProcessable } from '../../../core/api/service-result-processable';
import { BasePage, ToastMessageSeverity } from '../../../core/ui/pages/base-page';
import { TextInputControlComponent } from "../../components/text-input/text-input.component";
import { TextInputComponentInteractor } from '../../components/text-input/interactor/text-input.component.interactor';
import { PasswordInputControlComponent } from "../../components/password-input/password-input.component";
import { PasswordInputComponentInteractor } from '../../components/password-input/interactor/password-input.component.interactor';
import {ToastModule} from 'primeng/toast';
import { RippleModule } from 'primeng/ripple';
import { JwtService } from '../../../services/jwt-service/jwt-service';

@Component({
  selector: 'login-page',
  standalone: true,
  imports: [
    ReactiveFormsModule, 
    FormsModule, 
    LoadingSpinnerComponent, 
    TextInputControlComponent, 
    PasswordInputControlComponent,
    ToastModule,
    RippleModule,
  ],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css'
})
export class LoginPage extends BasePage<LoginPageModel> {

   loginServiceResultProcessable: IServiceResultProcessable<LoginOutputModel> =
      {
        processResult: (resultData: LoginOutputModel): boolean => {
          this.jwtService.set(resultData.jwtModel);

          if(!this.jwtService.isValid())
            return false;

          this.router.navigateByUrl('/home')

          return true;
        },
        processError: () => {
           this.showToastMessage(ToastMessageSeverity.Error, "", "Несъответствие на въведената електронна поща и/или парола");
        },
      };


  constructor(injector: Injector
      , private authenticationService: AuthenticationService
      , private jwtService: JwtService) {
      super(injector);
  }

  protected override loadData(): void {
  }

  protected override initControls(): void {
      this.basePageFormGroup = this.formBuilder.group({
          email: ['', [Validators.required, Validators.email]],
          password: ['', [Validators.required]],
    });
  }

  protected override createNewPageModel(): LoginPageModel {
    return new LoginPageModel();
  }

  protected override validateData(): boolean {

      if(!this.basePageFormGroup?.valid)
      {
          return false;
          this.showToastMessage(ToastMessageSeverity.Error, "", "Несъответствие на въведената електронна поща и/или парола");
      }

      return true;
  }

   protected override transferControlsToData(): void {
  }

  protected override onSubmitProcessable(): void {

    let userModel = new LoginInputModel();
    userModel.email = this.pageModel.emailInputControlInteractor.textValue;
    userModel.password = this.pageModel.passwordInputComponentInteractor.passwordValue;

      this.authenticationService.login(userModel, 
        this.loginServiceResultProcessable, this.pageAnimtaionController)
  }

}

class LoginPageModel{

    emailInputControlInteractor: TextInputComponentInteractor;
    passwordInputComponentInteractor: PasswordInputComponentInteractor;

    constructor() {
        this.emailInputControlInteractor = new TextInputComponentInteractor("email");
        this.passwordInputComponentInteractor = new PasswordInputComponentInteractor("password");
    }
    
}
