export class InvestmentGoal {
  id: number | undefined;
  yearsToTarget: number | undefined
  additionalBudgetAllocation: number | undefined;
  annualReturnRate_Percent: number | undefined;
  totalBalance: number | undefined
  rolloverCompletedGoals: boolean = false;
}

export class InvestmentTotals {
  addedDueToContribution: number | undefined;
  balanceAtTarget: number | undefined;
  totalInterestAccrued: number | undefined;
}
