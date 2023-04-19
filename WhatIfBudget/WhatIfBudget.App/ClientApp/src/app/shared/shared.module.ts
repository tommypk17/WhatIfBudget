import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { MenubarModule } from 'primeng/menubar';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { SplitButtonModule } from 'primeng/splitbutton';
import { MenuModule } from 'primeng/menu';
import { TabMenuModule } from 'primeng/tabmenu';
import { BudgetListingComponent } from './budget-listing/budget-listing.component';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { ListboxModule } from 'primeng/listbox';
import { FormsModule } from '@angular/forms';
import { BudgetEntryFormComponent } from './budget-entry-form/budget-entry-form.component';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ChartModule } from 'primeng/chart';
import { MortgageChartComponent } from './mortgage-chart/mortgage-chart.component';
import { SavingChartComponent } from './saving-chart/saving-chart.component';
import { InvestmentsChartComponent } from './investments-chart/investments-chart.component';
import { DebtChartComponent } from './debt-chart/debt-chart.component';
import { NetWorthChartComponent } from './net-worth-chart/net-worth-chart.component';

@NgModule({
  declarations: [
    NavigationComponent,
    BudgetListingComponent,
    BudgetEntryFormComponent,
    MortgageChartComponent,
    SavingChartComponent,
    InvestmentsChartComponent,
    DebtChartComponent,
    NetWorthChartComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MenubarModule,
    ButtonModule,
    SplitButtonModule,
    MenuModule,
    TabMenuModule,
    OverlayPanelModule,
    ListboxModule,
    InputTextModule,
    ProgressSpinnerModule,
    ChartModule,
  ],
  exports: [
    NavigationComponent,
    MortgageChartComponent,
    SavingChartComponent,
    InvestmentsChartComponent,
    DebtChartComponent,
    NetWorthChartComponent,
  ]
})
export class SharedModule { }
