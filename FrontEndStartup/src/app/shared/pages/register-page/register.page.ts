import { Component, Injector } from '@angular/core';
import { BasePage, ToastMessageSeverity } from '../../../core/ui/pages/base-page';
import { BasePageTemplateComponent } from "../../../core/ui/pages/base-page-template/base-page-template.component";
import { PasswordInputControlComponent } from "../../components/password-input/password-input.component";
import { TextInputControlComponent } from "../../components/text-input/text-input.component";
import { TextInputComponentInteractor } from '../../components/text-input/interactor/text-input.component.interactor';
import { PasswordInputComponentInteractor } from '../../components/password-input/interactor/password-input.component.interactor';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DateTimeInputControlInteractor } from '../../components/date-time-input-control/interactor/date-time-input-control.interactor';
import { DateTimeInputControlComponent } from '../../components/date-time-input-control/date-time-input-control.component';
import { AuthenticationService } from '../../../services/authentication-service/authentication.service';
import { IServiceResultProcessable } from '../../../core/api/service-result-processable';
import { RegisterInputModel, RegisterOutputModel } from '../../../services/authentication-service/models/authentication-models';
import { PrimeNGConfig } from 'primeng/api';
import { JwtService } from '../../../services/jwt-service/jwt-service';

@Component({
  selector: 'register-page',
  standalone: true,
  imports: [
    BasePageTemplateComponent,
    PasswordInputControlComponent,
    TextInputControlComponent,
    ReactiveFormsModule,
    FormsModule,
    DateTimeInputControlComponent
  ],
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.css']
})
export class RegisterPage extends BasePage<RegisterPageModel> {

  registerServiceResultProcessable: IServiceResultProcessable<RegisterOutputModel> =
    {
      processResult: (resultData: RegisterOutputModel): boolean => {
        this.jwtService.set(resultData.jwtModel);

        if (!this.jwtService.isValid()) {
          return false;
        }

        this.router.navigateByUrl('/home');
        return true;
      },
      processError: () => {
        this.showToastMessage(ToastMessageSeverity.Error, "", "Несъответствие на въведената електронна поща и/или парола");
      },
    };

  private authenticationService: AuthenticationService;
  private jwtService: JwtService;

  constructor(injector: Injector) {
    super(injector);
    this.authenticationService = injector.get(AuthenticationService);
    this.jwtService = injector.get(JwtService);
  }

  protected override loadData(): void {
    // Може да добавите логика за зареждане на данни тук
  }

  protected override initControls(): void {
    // Създаване на FormGroup с валидация за задължителни полета
    this.basePageFormGroup = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      middleName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      password: ['', [Validators.required]],
      repeatPassword: ['', [Validators.required]],
    });
  }

  protected override createNewPageModel(): RegisterPageModel {
    return new RegisterPageModel();
  }

  protected override validateData(): boolean {
    if (!this.basePageFormGroup?.valid) {
      this.showToastMessage(ToastMessageSeverity.Error, "", "Попълнете всички задължителни полета.");
      return false;
    }

    // Допълнителна логика за валидация, ако е необходимо
    return true;
  }

  protected override transferControlsToData(): void {
    // Може да добавите логика за прехвърляне на стойности от формата към модела
  }

  protected override onSubmitProcessable(): void {
    let registerInputModel = new RegisterInputModel();
    registerInputModel.firstName = this.pageModel.firtsNameInputControlInteractor.textValue;
    registerInputModel.middleName = this.pageModel.middleNameInputControlInteractor.textValue;
    registerInputModel.lastName = this.pageModel.lastNameInputControlInteractor.textValue;
    registerInputModel.email = this.pageModel.emailInputControlInteractor.textValue;
    registerInputModel.phone = this.pageModel.phoneNumberInputControlInteractor.textValue;
    registerInputModel.password = this.pageModel.passwordInputComponentInteractor.passwordValue;
    registerInputModel.confirmedPassword = this.pageModel.confirmedPasswordInputComponentInteractor.passwordValue;
    registerInputModel.dateOfBirth = this.pageModel.dateOfBirthInputControlInteractor.dateTimeValue;

    // Викаме регистрационната услуга
    this.authenticationService.register(registerInputModel, 
      this.registerServiceResultProcessable, this.pageAnimtaionController);
  }
}

class RegisterPageModel {

  firtsNameInputControlInteractor: TextInputComponentInteractor;
  middleNameInputControlInteractor: TextInputComponentInteractor;
  lastNameInputControlInteractor: TextInputComponentInteractor;
  phoneNumberInputControlInteractor: TextInputComponentInteractor;
  emailInputControlInteractor: TextInputComponentInteractor;
  passwordInputComponentInteractor: PasswordInputComponentInteractor;
  confirmedPasswordInputComponentInteractor: PasswordInputComponentInteractor;
  dateOfBirthInputControlInteractor: DateTimeInputControlInteractor;

  constructor() {
    this.firtsNameInputControlInteractor = new TextInputComponentInteractor("firstName");
    this.middleNameInputControlInteractor = new TextInputComponentInteractor("middleName");
    this.lastNameInputControlInteractor = new TextInputComponentInteractor("lastName");
    this.phoneNumberInputControlInteractor = new TextInputComponentInteractor("phoneNumber");
    this.emailInputControlInteractor = new TextInputComponentInteractor("email");
    this.passwordInputComponentInteractor = new PasswordInputComponentInteractor("password");
    this.confirmedPasswordInputComponentInteractor = new PasswordInputComponentInteractor("confirmedPassword");
    this.dateOfBirthInputControlInteractor = new DateTimeInputControlInteractor("dateOfBirth");
  }
}
