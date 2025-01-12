import { BaseComponentInteractor } from "../../../../core/ui/interactors/base-component-intercator";
import { Product } from "../../../domain/products/products-model";

export class ProductCardComponentInteractor extends BaseComponentInteractor{

    private _product: Product

    constructor() {
        super();
        this._product = new Product();   
    }

    public get product(): Product{
        return this._product
    }

    public setProductData(product: Product): void{
        this._product = product;
    }
}