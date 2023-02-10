import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExpenseRoutingModule } from './expense-routing.module';
import { ExpenseComponent } from './pages/expense/expense.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { IncomeDetailSectionComponent } from '../income/components/income-detail-section/income-detail-section.component';
import { IncomeMonthlyDetailSectionComponent } from '../income/components/income-monthly-detail-section/income-monthly-detail-section.component';
import { ExpenseDetailSectionComponent } from './components/expense-detail-section/expense-detail-section.component';

@NgModule({
  declarations: [
    ExpenseComponent,
    IncomeDetailSectionComponent,
    IncomeMonthlyDetailSectionComponent,
    ExpenseDetailSectionComponent
  ],
  imports: [
    CommonModule,
    ExpenseRoutingModule,

    CardModule,
    ButtonModule
  ]
})
export class ExpenseModule { }
