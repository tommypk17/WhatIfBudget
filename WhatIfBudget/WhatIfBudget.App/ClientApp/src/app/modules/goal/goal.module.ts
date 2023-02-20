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
import { GoalNavigationComponent } from './components/goal-navigation/goal-navigation.component';
import { TabMenuModule } from 'primeng/tabmenu';
import { InvestmentComponent } from './pages/investment/investment.component';
import { SavingComponent } from './pages/saving/saving.component';
import { DebtComponent } from './pages/debt/debt.component';
import { MortgageComponent } from './pages/mortgage/mortgage.component';

@NgModule({
  declarations: [
    GoalComponent,
    InvestmentEntryFormComponent,
    GoalNavigationComponent,
    InvestmentComponent,
    SavingComponent,
    DebtComponent,
    MortgageComponent,
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
    TabMenuModule
  ],
  exports: [
    GoalNavigationComponent
    ]
})
export class GoalModule { }
