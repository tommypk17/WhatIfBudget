import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { LoadBudgetGuard } from './guards/load-budget.guard';
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [
  { path: '', loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule) },
  { path: 'incomes', loadChildren: () => import('./modules/income/income.module').then(m => m.IncomeModule), canActivate: [LoginGuard, MsalGuard, LoadBudgetGuard] },
  { path: 'expenses', loadChildren: () => import('./modules/expense/expense.module').then(m => m.ExpenseModule), canActivate: [LoginGuard, MsalGuard, LoadBudgetGuard] },
  { path: 'goals', loadChildren: () => import('./modules/goal/goal.module').then(m => m.GoalModule), canActivate: [LoginGuard, MsalGuard, LoadBudgetGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
