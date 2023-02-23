import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestmentGoalListingComponent } from './investment-goal-listing.component';

describe('InvestmentGoalListingComponent', () => {
  let component: InvestmentGoalListingComponent;
  let fixture: ComponentFixture<InvestmentGoalListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvestmentGoalListingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvestmentGoalListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
