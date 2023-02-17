import { EFrequency } from "../enums/efrequency";

export class Investment {
  id: number | undefined;
  name: string | undefined;
  amount: number | undefined;
  frequency: EFrequency | undefined;
}
