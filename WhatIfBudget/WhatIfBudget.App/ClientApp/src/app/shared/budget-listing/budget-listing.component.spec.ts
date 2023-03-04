import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetListingComponent } from './budget-listing.component';

describe('BudgetListingComponent', () => {
  let component: BudgetListingComponent;
  let fixture: ComponentFixture<BudgetListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetListingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BudgetListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
