import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { LoadBudgetGuard } from './guards/load-budget.guard';
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [
  { path: '', loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule) },
  { path: 'incomes', loadChildren: () => import('./modules/income/income.module').then(m => m.IncomeModule), canActivate: [LoginGuard, LoadBudgetGuard], canLoad: [MsalGuard] },
  { path: 'expenses', loadChildren: () => import('./modules/expense/expense.module').then(m => m.ExpenseModule), canActivate: [LoginGuard, LoadBudgetGuard], canLoad: [MsalGuard] },
  { path: 'goals', loadChildren: () => import('./modules/goal/goal.module').then(m => m.GoalModule), canActivate: [LoginGuard, LoadBudgetGuard], canLoad: [MsalGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
