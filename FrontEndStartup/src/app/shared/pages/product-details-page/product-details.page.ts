import { Component } from '@angular/core';
import { BasePageTemplateComponent } from '../../../core/ui/pages/base-page-template/base-page-template.component';
import { BasePage } from '../../../core/ui/pages/base-page';

@Component({
  selector: 'product-details-page',
  standalone: true,
  imports: [BasePageTemplateComponent],
  templateUrl: './product-details.page.html',
  styleUrl: './product-details.page.css'
})
export class ProductDetailsPage extends BasePage<ProductDetailsPageModel> {

  protected override initControls(): void {
  }

  protected override loadData(): void {
  }

  protected override createNewPageModel(): ProductDetailsPageModel {
    return new ProductDetailsPageModel();
  }

   protected override transferControlsToData(): void {
  }

  protected override validateData(): boolean {
      return true;
  }

   protected override onSubmitProcessable(): void {
    
   }
  
}

class ProductDetailsPageModel{

}
