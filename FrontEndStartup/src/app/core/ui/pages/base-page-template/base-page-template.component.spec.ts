import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasePageTemplateComponent } from './base-page-template.component';

describe('BasePageTemplateComponent', () => {
  let component: BasePageTemplateComponent;
  let fixture: ComponentFixture<BasePageTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BasePageTemplateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BasePageTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
