import { Component } from '@angular/core';
import { BaseComponent } from '../../base/ui/components/base-component';
import { ProductCardComponentInteractor } from './interactor/product-card.interactor';
import { CommonModule } from '@angular/common';
 
@Component({
  selector: 'product-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent extends BaseComponent<ProductCardComponentInteractor> {
} 
