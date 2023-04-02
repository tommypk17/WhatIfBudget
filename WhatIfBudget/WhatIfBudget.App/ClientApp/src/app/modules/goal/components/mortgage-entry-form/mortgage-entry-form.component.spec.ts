import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MortgageEntryFormComponent } from './mortgage-entry-form.component';

describe('MortgageEntryFormComponent', () => {
  let component: MortgageEntryFormComponent;
  let fixture: ComponentFixture<MortgageEntryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MortgageEntryFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MortgageEntryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
