import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';

@Component({
  selector: 'login-page',
  standalone: true,
  imports: [],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css'
})
export class LoginPage extends BasePage<LoginPageModel> {

  protected override loadData(): void {
  }

  protected override initControls(): void {
  }

  protected override createNewPageModel(): LoginPageModel {
    return new LoginPageModel();
  }
}

class LoginPageModel{

}
