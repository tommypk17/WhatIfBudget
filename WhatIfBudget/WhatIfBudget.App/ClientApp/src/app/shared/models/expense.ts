import { EFrequency } from "../enums/efrequency";
import { EPriority } from "../enums/epriority";

export class Expense {
  id: number | undefined;
  name: string | undefined;
  amount: number | undefined;
  frequency: EFrequency | undefined;
  priority: EPriority | undefined;
}
