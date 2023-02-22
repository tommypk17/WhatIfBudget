import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestmentListingComponent } from './investment-listing.component';

describe('InvestmentListingComponent', () => {
  let component: InvestmentListingComponent;
  let fixture: ComponentFixture<InvestmentListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvestmentListingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvestmentListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
