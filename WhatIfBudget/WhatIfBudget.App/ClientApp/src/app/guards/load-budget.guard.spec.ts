import { TestBed } from '@angular/core/testing';

import { LoadBudgetGuard } from './load-budget.guard';

describe('LoadBudgetGuard', () => {
  let guard: LoadBudgetGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(LoadBudgetGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
