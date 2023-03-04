import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IncomeRoutingModule } from './income-routing.module';
import { IncomeComponent } from './pages/income/income.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { IncomeEntryFormComponent } from './components/income-entry-form/income-entry-form.component';

import { FormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { IncomeListingComponent } from './components/income-listing/income-listing.component';
import { IncomeDetailSectionComponent } from './components/income-detail-section/income-detail-section.component';
import { IncomeMonthlyDetailSectionComponent } from './components/income-monthly-detail-section/income-monthly-detail-section.component';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { ConfirmationService } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';

@NgModule({
  declarations: [
    IncomeComponent,
    IncomeEntryFormComponent,
    IncomeListingComponent,
    IncomeDetailSectionComponent,
    IncomeMonthlyDetailSectionComponent,
  ],
  imports: [
    CommonModule,
    IncomeRoutingModule,
    FormsModule,

    CardModule,
    ButtonModule,
    InputTextModule,
    InputNumberModule,
    DropdownModule,
    TableModule,
    ConfirmPopupModule,
    DialogModule
  ],
  providers: [ConfirmationService]
})
export class IncomeModule { }
