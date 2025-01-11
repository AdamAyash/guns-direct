import { Component } from '@angular/core';
import { ProductCardComponentInteractor } from './interactor/product-card.interactor';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { BaseComponent } from '../../../core/ui/components/base-component';

@Component({
  selector: 'product-card',
  standalone: true,
  imports: [CommonModule, RouterModule, RouterOutlet],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent extends BaseComponent<ProductCardComponentInteractor> {
} 
