import { BaseUIComponentInteractor } from "../../../../core/ui/interactors/base-ui-component-interactor";

export class CarouselComponentInteractor extends BaseUIComponentInteractor{

    private readonly _itemsPerActiveGroupDefault = 3;

    private _itemsPerActiveGroup: number = this._itemsPerActiveGroupDefault;
    private _carouselItemsDataArray: Array<any[]> = new Array<any[]>();

    private _maxCarouselGroups: number = 3;
    private _totalCarouselGroupsCount: number = 0;

   public get itemsPerActiveGroup() : number {
    return this._itemsPerActiveGroup;
   }
   public set itemsPerActiveGroup(v : number) {
    this._itemsPerActiveGroup = v;
   }

   public get carouselItemsDataArray():  Array<any[]>{
    return this._carouselItemsDataArray;
   }

public get totalCarouselGroupsCount(): number{
    return this._totalCarouselGroupsCount;
   }

   public  setCarouselItemDataArray<ItemType>(itemsDataArray: ItemType[]){

    const totalCarouselGroupsCount: number = itemsDataArray.length / this._itemsPerActiveGroup;
    this._totalCarouselGroupsCount = totalCarouselGroupsCount;

    for(let groupIndex = 0; groupIndex < totalCarouselGroupsCount; groupIndex++){
        this._carouselItemsDataArray.push(itemsDataArray.slice(0, this._itemsPerActiveGroup));
    }

   }
}