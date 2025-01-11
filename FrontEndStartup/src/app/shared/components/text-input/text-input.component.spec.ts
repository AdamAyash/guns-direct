import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextInputControlComponent } from './text-input.component';

describe('TextInputComponent', () => {
  let component: TextInputControlComponent;
  let fixture: ComponentFixture<TextInputControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TextInputControlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TextInputControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
