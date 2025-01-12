import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasswordInputControlComponent } from './password-input.component';

describe('PasswordInputComponent', () => {
  let component: PasswordInputControlComponent;
  let fixture: ComponentFixture<PasswordInputControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasswordInputControlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasswordInputControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
