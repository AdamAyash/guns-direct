import { Component, Input, TemplateRef } from '@angular/core';
import { CarouselComponentInteractor } from './interactor/carousel.component.interactor';
import { CommonModule } from '@angular/common';
import { BaseComponent } from '../../../core/ui/components/base-component';

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
