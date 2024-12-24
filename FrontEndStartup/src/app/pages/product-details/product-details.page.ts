import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { BasePageTemplateComponent } from "../../base/ui/pages/base-page-template/base-page-template.component";

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
  
}

class ProductDetailsPageModel{

}
