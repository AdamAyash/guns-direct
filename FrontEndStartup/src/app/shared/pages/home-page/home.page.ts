import { Component, Injector } from '@angular/core';
import { NavigatorMenuComponent } from '../../components/navigator-menu/navigator-menu.component';
import { NavigatorMenuIntercator } from '../../components/navigator-menu/interactors/navigator-menu.interactor';
import { ProductCardComponent } from '../../components/product-card/product-card.component';
import { RouterOutlet, RouterModule } from '@angular/router';
import { Product } from '../../domain/products/products-model';
import { ProductCardComponentInteractor } from '../../components/product-card/interactor/product-card.interactor';
import { CarouselComponent } from '../../components/carousel/carousel.component';
import { CarouselComponentInteractor } from '../../components/carousel/interactor/carousel.component.interactor';
import { BasePageTemplateComponent } from '../../../core/ui/pages/base-page-template/base-page-template.component';
import { BasePage } from '../../../core/ui/pages/base-page';
import { IServiceResultProcessable } from '../../../core/api/service-result-processable';
import { GetAllProductOutputModel } from '../../../services/products-data-service/models/get-all-products-models';
import { ProductsDataService } from '../../../services/products-data-service/products-data.service';

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

  constructor(injector: Injector
    , private productsDataService: ProductsDataService) {
    super(injector);
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


 protected override transferControlsToData(): void {
  }
  protected override validateData(): boolean {
      return true;
  }

   protected override onSubmitProcessable(): void {
      
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
