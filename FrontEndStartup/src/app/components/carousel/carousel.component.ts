import { Component, Input, TemplateRef } from '@angular/core';
import { BaseComponent } from '../../base/ui/components/base-component';
import { CarouselComponentInteractor } from './interactor/carousel.component.interactor';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'carousel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css'
})
export class CarouselComponent extends BaseComponent<CarouselComponentInteractor> {
      @Input() contentTemplate!: TemplateRef<any>;
}
