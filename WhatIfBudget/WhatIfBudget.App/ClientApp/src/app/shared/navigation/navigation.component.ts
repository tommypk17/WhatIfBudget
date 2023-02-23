import { Component, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { BudgetListingComponent } from '../budget-listing/budget-listing.component';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
  providers: [DialogService]
})
export class NavigationComponent implements OnInit {

  constructor(private dialogService: DialogService) { }

  navs: MenuItem[] = [];

  ngOnInit() {
    this.navs = [
      { label: "Home", icon: "pi pi-home", routerLink: "/" },
      { label: "Incomes", icon: "pi pi-money-bill", routerLink: "/incomes" },
      { label: "Expenses", icon: "pi pi-chart-pie", routerLink: "/expenses" },
      { label: "Goals", icon: "pi pi-chart-line", routerLink: "/goals" },
      {
        label: "Budgets", icon: "pi pi-list", items:
          [{ label: 'Start New Budget', icon: 'pi pi-fw pi-refresh' },
          { label: 'Save Current Budget', icon: 'pi pi-fw pi-save' },
          { label: 'Load Saved Budget', icon: 'pi pi-fw pi-download', command: () => { this.budgetlist() } },
          { label: 'Log Out', icon: 'pi pi-fw pi-sign-out' }]
      }
    ];
  }

  budgetlist() {
    const ref = this.dialogService.open(BudgetListingComponent, {
      header: 'Saved Budgets',
      width: '70%'
    });
  }
}
