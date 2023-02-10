import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeListingComponent } from './income-listing.component';

describe('IncomeListingComponent', () => {
  let component: IncomeListingComponent;
  let fixture: ComponentFixture<IncomeListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomeListingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomeListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
