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
import { InvestmentGoalEntryFormComponent } from './components/investment-goal-entry-form/investment-goal-entry-form.component';
import { InvestmentListingComponent } from './components/investment-listing/investment-listing.component';
import { InvestmentGoalListingComponent } from './components/investment-goal-listing/investment-goal-listing.component';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { ConfirmationService } from 'primeng/api';
import { MortgageEntryFormComponent } from './components/mortgage-entry-form/mortgage-entry-form.component';
import { MortgageChartComponent } from './components/mortgage-chart/mortgage-chart.component';
import { ChartModule } from 'primeng/chart';
import { SavingEntryFormComponent } from './components/saving-entry-form/saving-entry-form.component';

@NgModule({
  declarations: [
    GoalComponent,
    InvestmentEntryFormComponent,
    GoalNavigationComponent,
    InvestmentComponent,
    SavingComponent,
    DebtComponent,
    MortgageComponent,
    InvestmentGoalEntryFormComponent,
    InvestmentListingComponent,
    InvestmentGoalListingComponent,
    MortgageEntryFormComponent,
    MortgageChartComponent,
    SavingEntryFormComponent,
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
    TabMenuModule,
    TableModule,
    DialogModule,
    ConfirmPopupModule,
    ChartModule
  ],
  exports: [
    GoalNavigationComponent
  ],
  providers: [ConfirmationService]
})
export class GoalModule { }
