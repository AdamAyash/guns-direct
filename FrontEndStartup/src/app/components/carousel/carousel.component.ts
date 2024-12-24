import { Component } from '@angular/core';
import { BaseComponent } from '../../base/ui/components/base-component';
import { CarouselComponentInteractor } from './interactor/carousel.component.interactor';

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css'
})
export class CarouselComponent extends BaseComponent<CarouselComponentInteractor> {

}
