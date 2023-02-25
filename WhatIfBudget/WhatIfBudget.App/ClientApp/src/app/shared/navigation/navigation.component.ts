import { Component, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { BudgetListingComponent } from '../budget-listing/budget-listing.component';
import { DialogService } from 'primeng/dynamicdialog';
import { BudgetEntryFormComponent } from '../budget-entry-form/budget-entry-form.component';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
  providers: [DialogService]
})
export class NavigationComponent implements OnInit {

  constructor(private dialogService: DialogService, private sharedService: SharedService) { }

  navs: MenuItem[] = [];

  ngOnInit() {
    this.navs = [
      { label: 'Load Budget', icon: 'pi pi-fw pi-download', command: () => { this.budgetlist() } },
      { label: 'Start New Budget', icon: 'pi pi-fw pi-refresh', command: () => { this.budgetCreate() } }
    ];
    if (this.sharedService.budgetLoaded) {
      this.navs = this.loadFullNavigation();
    }
    this.sharedService.budgetLoadedEmit.subscribe(() => {
      if (this.sharedService.budgetLoaded) this.navs = this.loadFullNavigation();
    });
  }

  private loadFullNavigation(): MenuItem[] {
    return [
      { label: "Home", icon: "pi pi-home", routerLink: "/" },
      { label: "Incomes", icon: "pi pi-money-bill", routerLink: "/incomes" },
      { label: "Expenses", icon: "pi pi-chart-pie", routerLink: "/expenses" },
      { label: "Goals", icon: "pi pi-chart-line", routerLink: "/goals" },
      {
        label: "Budgets", icon: "pi pi-list", items:
          [{ label: 'Start New Budget', icon: 'pi pi-fw pi-refresh', command: () => { this.budgetCreate() } },
          { label: 'Load Saved Budget', icon: 'pi pi-fw pi-download', command: () => { this.budgetlist() } }]
      }
    ];
  }

  budgetlist() {
    const ref = this.dialogService.open(BudgetListingComponent, {
      header: 'Saved Budgets',
    });
  }

  budgetCreate() {
    const ref = this.dialogService.open(BudgetEntryFormComponent, {
      header: 'New Budget',
    });
  }

  logout(): void {
    this.sharedService.logout();
  }
}
