import { TestBed } from '@angular/core/testing';
import { InvestmentGoalService } from './investment.goal.service';


describe('InvestmentService', () => {
  let service: InvestmentGoalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InvestmentGoalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
