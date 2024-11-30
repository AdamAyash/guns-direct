export class Product{
    id: number;
    name: string;
    price: number;
    category: string;
    quantity: number;
    imageURL: string;

    constructor(
        id: number,
        name: string,
        price: number,
        category: string,
        quantity: number,
        imageURL: string 
    ) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.category = category;
        this.quantity = quantity;
        this.imageURL = imageURL;
    }
}
