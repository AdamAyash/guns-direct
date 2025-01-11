import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsListPage } from './products-list.page';

describe('ProductsListPage', () => {
  let component: ProductsListPage;
  let fixture: ComponentFixture<ProductsListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductsListPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductsListPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
