import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestmentsChartComponent } from './investments-chart.component';

describe('InvestmentsChartComponent', () => {
  let component: InvestmentsChartComponent;
  let fixture: ComponentFixture<InvestmentsChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvestmentsChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvestmentsChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
