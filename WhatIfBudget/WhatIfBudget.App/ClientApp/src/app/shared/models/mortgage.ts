export class Mortgage {
  id: number | undefined;
  totalBalance: number | undefined;
  interestRate_Percent: number | undefined;
  monthlyPayment: number | undefined;
  estimatedCurrentValue: number | undefined;
  additionalBudgetAllocation: number | undefined;
}

export class MortgageTotals {
  monthsToPayoff: number | undefined;
  totalInterestAccrued: number | undefined;
  totalCostToPayoff: number | undefined;
  allocationSavings: number | undefined;
}
