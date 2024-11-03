import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigatorMenuComponent } from './components/navigator-menu/navigator-menu.component';
import { NavigatorMenuIntercator } from './components/navigator-menu/interactors/navigator-menu.interactor';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavigatorMenuComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Guns Direct';

  interactor: NavigatorMenuIntercator;

constructor(){
  this.interactor = new NavigatorMenuIntercator();
}

}
