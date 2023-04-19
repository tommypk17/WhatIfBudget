import { KeyValue } from "@angular/common";

export class Budget {
  id: number | undefined;
  name: string | undefined;
  savingGoalId: number | undefined;
  debtGoalId: number | undefined;
  mortgageGoalId: number | undefined;
  investmentGoalId: number | undefined;
}

export class AdditionalContributions {
  debtGoal: number | undefined;
  mortgageGoal: number | undefined;
  savingGoal: number | undefined;
  investmentGoal: number | undefined;
}

export class NetValue {
  balance: KeyValue<number, number>[] = [];
  savingGoalMonth: number | undefined;
  debtGoalMonth: number | undefined;
  mortgageGoalMonth: number | undefined;
}
