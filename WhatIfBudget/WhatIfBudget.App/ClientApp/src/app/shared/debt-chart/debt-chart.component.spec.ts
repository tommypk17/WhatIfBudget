import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DebtChartComponent } from './debt-chart.component';

describe('DebtChartComponent', () => {
  let component: DebtChartComponent;
  let fixture: ComponentFixture<DebtChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DebtChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DebtChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
