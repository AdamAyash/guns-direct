import { Product } from "../../../shared/domain/products/products-model";

export class GetProductByIdInputModel {
    productId?: string;
}

export class GetProductByIdOutputtModel {
    productData?: Product
}