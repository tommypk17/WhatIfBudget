import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeDetailSectionComponent } from './income-detail-section.component';

describe('IncomeDetailSectionComponent', () => {
  let component: IncomeDetailSectionComponent;
  let fixture: ComponentFixture<IncomeDetailSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomeDetailSectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomeDetailSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
