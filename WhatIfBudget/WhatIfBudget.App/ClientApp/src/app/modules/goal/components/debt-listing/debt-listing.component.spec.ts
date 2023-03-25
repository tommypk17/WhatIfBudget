import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DebtListingComponent } from './debt-listing.component';

describe('DebtListingComponent', () => {
  let component: DebtListingComponent;
  let fixture: ComponentFixture<DebtListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DebtListingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DebtListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
