import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule) },
  { path: 'incomes', loadChildren: () => import('./modules/income/income.module').then(m => m.IncomeModule) },
  { path: 'expenses', loadChildren: () => import('./modules/expense/expense.module').then(m => m.ExpenseModule) },
  { path: 'goals', loadChildren: () => import('./modules/goal/goal.module').then(m => m.GoalModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
