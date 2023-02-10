import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExpenseRoutingModule } from './expense-routing.module';
import { ExpenseComponent } from './pages/expense/expense.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { ExpenseDetailSectionComponent } from './components/expense-detail-section/expense-detail-section.component';

@NgModule({
  declarations: [
    ExpenseComponent,
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
