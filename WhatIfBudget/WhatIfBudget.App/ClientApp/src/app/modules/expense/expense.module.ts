import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExpenseRoutingModule } from './expense-routing.module';
import { ExpenseComponent } from './pages/expense/expense.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { ExpenseDetailSectionComponent } from './components/expense-detail-section/expense-detail-section.component';
import { ExpenseEntryFormComponent } from './components/expense-entry-form/expense-entry-form.component';
import { ExpenseListingComponent } from './components/expense-listing/expense-listing.component';
import { TableModule } from 'primeng/table';
import { NeedWantFilterPipe } from './pipes/need-want-filter.pipe';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmationService } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { ConfirmPopupModule } from 'primeng/confirmpopup';

@NgModule({
  declarations: [
    ExpenseComponent,
    ExpenseDetailSectionComponent,
    ExpenseEntryFormComponent,
    ExpenseListingComponent,
    NeedWantFilterPipe
  ],
  imports: [
    CommonModule,
    ExpenseRoutingModule,
    FormsModule,

    CardModule,
    ButtonModule,
    InputTextModule,
    InputNumberModule,
    DropdownModule,
    TableModule,
    DialogModule,
    ConfirmPopupModule
  ],
  providers: [ConfirmationService]
})
export class ExpenseModule { }
