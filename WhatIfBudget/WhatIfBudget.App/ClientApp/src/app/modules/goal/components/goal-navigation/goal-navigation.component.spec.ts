import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoalNavigationComponent } from './goal-navigation.component';

describe('GoalNavigationComponent', () => {
  let component: GoalNavigationComponent;
  let fixture: ComponentFixture<GoalNavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GoalNavigationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GoalNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
