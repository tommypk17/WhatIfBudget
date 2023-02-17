import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GoalRoutingModule } from './goal-routing.module';
import { GoalComponent } from './pages/goal/goal.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { SliderModule } from 'primeng/slider';
import { InvestmentEntryFormComponent } from './components/investment-entry-form/investment-entry-form.component';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  declarations: [
    GoalComponent,
    InvestmentEntryFormComponent
  ],
  imports: [
    CommonModule,
    GoalRoutingModule,
    FormsModule,

    CardModule,
    ButtonModule,
    SliderModule,
    DropdownModule,
    InputTextModule,
    InputNumberModule,
  ]
})
export class GoalModule { }
