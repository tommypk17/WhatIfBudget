import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DebtComponent } from './pages/debt/debt.component';
import { GoalComponent } from './pages/goal/goal.component';
import { InvestmentComponent } from './pages/investment/investment.component';
import { MortgageComponent } from './pages/mortgage/mortgage.component';
import { SavingComponent } from './pages/saving/saving.component';

const routes: Routes = [
  { path: '', component: GoalComponent },
  { path: 'savings', component: SavingComponent },
  { path: 'debts', component: DebtComponent },
  { path: 'mortgages', component: MortgageComponent },
  { path: 'investments', component: InvestmentComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GoalRoutingModule { }
