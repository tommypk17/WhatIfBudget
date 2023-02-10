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

@NgModule({
  declarations: [
    IncomeComponent,
    IncomeEntryFormComponent,
    IncomeListingComponent
  ],
  imports: [
    CommonModule,
    IncomeRoutingModule,
    FormsModule,

    CardModule,
    ButtonModule,
    InputNumberModule,
    DropdownModule,
    TableModule
  ]
})
export class IncomeModule { }
