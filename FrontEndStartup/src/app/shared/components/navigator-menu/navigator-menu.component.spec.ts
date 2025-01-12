import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigatorMenuComponent } from './navigator-menu.component';

describe('NavigatorMenuComponent', () => {
  let component: NavigatorMenuComponent;
  let fixture: ComponentFixture<NavigatorMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavigatorMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavigatorMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
