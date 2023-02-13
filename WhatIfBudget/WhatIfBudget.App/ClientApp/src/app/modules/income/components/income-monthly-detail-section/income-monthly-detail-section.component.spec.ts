import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeMonthlyDetailSectionComponent } from './income-monthly-detail-section.component';

describe('IncomeMonthlyDetailSectionComponent', () => {
  let component: IncomeMonthlyDetailSectionComponent;
  let fixture: ComponentFixture<IncomeMonthlyDetailSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomeMonthlyDetailSectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomeMonthlyDetailSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
