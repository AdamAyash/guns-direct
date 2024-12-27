import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { AuthenticationService } from '../../services/authentication-service/authentication.service';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';
import { LoginInputModel, LoginOutputModel } from '../../services/authentication-service/models/authentication-models';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoadingSpinnerComponent } from "../../components/loading-spinner/loading-spinner.component";

@Component({
  selector: 'login-page',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, LoadingSpinnerComponent],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css'
})
export class LoginPage extends BasePage<LoginPageModel> {

   loginServiceResultProcessable: IServiceResultProcessable<LoginOutputModel> =
      {
        processResult: (resultData: LoginOutputModel): boolean => {
  
          return true;
        },
        processError: () => {},
      };


  constructor(private authenticationService: AuthenticationService) {
    super();
  }

  protected override loadData(): void {
  }

  protected override initControls(): void {
  }

  protected override createNewPageModel(): LoginPageModel {
    return new LoginPageModel();
  }

  public override onSubmit(){

    let userModel = new LoginInputModel();
    userModel.email = 'randommail@mail.com';
    userModel.password = '190504';

      this.authenticationService.login(userModel, 
          this.loginServiceResultProcessable, this.pageAnimtaionController);
  }

}

class LoginPageModel{

}
