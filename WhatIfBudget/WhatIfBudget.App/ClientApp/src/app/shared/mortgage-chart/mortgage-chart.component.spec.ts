import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MortgageChartComponent } from './mortgage-chart.component';

describe('MortgageChartComponent', () => {
  let component: MortgageChartComponent;
  let fixture: ComponentFixture<MortgageChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MortgageChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MortgageChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
