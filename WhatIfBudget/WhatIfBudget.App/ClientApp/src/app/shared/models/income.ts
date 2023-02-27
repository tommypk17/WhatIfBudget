import { EFrequency } from "../enums/efrequency";

export class Income {
  id: number | undefined;
  name: string | undefined;
  amount: number | undefined;
  frequency: EFrequency | undefined;
  budgetId: number | undefined;
}
