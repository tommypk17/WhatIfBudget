import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IncomeRoutingModule } from './income-routing.module';
import { IncomeComponent } from './pages/income/income.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { IncomeListingComponent } from './components/income-listing/income-listing.component';
import { IncomeEntryFormComponent } from './components/income-entry-form/income-entry-form.component';
import { IncomeDetailSectionComponent } from './components/income-detail-section/income-detail-section.component';

import { FormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  declarations: [
    IncomeComponent,
    IncomeListingComponent,
    IncomeEntryFormComponent,
    IncomeDetailSectionComponent
  ],
  imports: [
    CommonModule,
    IncomeRoutingModule,
    FormsModule,

    CardModule,
    ButtonModule,
    InputNumberModule,
    DropdownModule
  ]
})
export class IncomeModule { }
