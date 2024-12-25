import { Component } from '@angular/core';
import { BasePage } from '../../base/ui/pages/base-page';
import { NavigatorMenuComponent } from '../../components/navigator-menu/navigator-menu.component';
import { NavigatorMenuIntercator } from '../../components/navigator-menu/interactors/navigator-menu.interactor';
import { ProductCardComponent } from '../../components/product-card/product-card.component';
import { RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';
import { BasePageTemplateComponent } from '../../base/ui/pages/base-page-template/base-page-template.component';
import { ProductsDataService } from '../../services/products-data-service/products-data.service';
import { Product } from '../../domain/products/products-model';
import { IServiceResultProcessable } from '../../base/server/service-result-processable';
import { GetAllProductOutputModel } from '../../services/products-data-service/models/get-all-products-models';
import { ProductCardComponentInteractor } from '../../components/product-card/interactor/product-card.interactor';
import { CarouselComponent } from '../../components/carousel/carousel.component';
import { CarouselComponentInteractor } from '../../components/carousel/interactor/carousel.component.interactor';

@Component({
  selector: 'home-page',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    BasePageTemplateComponent,
    ProductCardComponent,
    CarouselComponent,
  ],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css',
})
export class HomePage extends BasePage<HomePageModel> {
  public _productsList?: Product[];
  public _productCarouselItemsArray: Array<Product[]> = new Array<Product[]>();

  getAllProductsServiceResultProcessable: IServiceResultProcessable<GetAllProductOutputModel> =
    {
      processResult: (resultData: GetAllProductOutputModel): boolean => {
        if (!resultData.products) 
              return true;

        this._productsList = resultData.products;

        this.pageModel.productCardComponentInteractor.setProductData(resultData.products[0]);
        this.pageModel.carouselComponentInteractor.setCarouselItemDataArray(resultData.products);

        return true;
      },
      processError: () => {},
    };

  constructor(private productsDataService: ProductsDataService) {
    super();
  }

  protected override loadData(): void {
    this.productsDataService.getAllProducts(
      this.getAllProductsServiceResultProcessable,
      this.pageAnimtaionController
    );
  }

  protected override initControls(): void {
    
      this.pageModel.carouselComponentInteractor.itemsPerActiveGroup = 7;
  }

  protected override createNewPageModel(): HomePageModel {
    return new HomePageModel();
  }

  private setProductCarouselData(): void {}
}

class HomePageModel {
  productCardComponentInteractor: ProductCardComponentInteractor;
  carouselComponentInteractor: CarouselComponentInteractor;

  constructor() {
    this.productCardComponentInteractor = new ProductCardComponentInteractor();
    this.carouselComponentInteractor = new CarouselComponentInteractor("productsCarousel");
  }
}
