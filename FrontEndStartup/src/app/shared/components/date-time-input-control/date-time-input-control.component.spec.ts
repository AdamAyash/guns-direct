import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateTimeInputControlComponent } from './date-time-input-control.component';

describe('DateTimeInputControlComponent', () => {
  let component: DateTimeInputControlComponent;
  let fixture: ComponentFixture<DateTimeInputControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DateTimeInputControlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateTimeInputControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
