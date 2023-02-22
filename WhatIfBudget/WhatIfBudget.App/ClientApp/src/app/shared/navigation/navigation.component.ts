import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  constructor() { }

  navs: MenuItem[] = [];

  ngOnInit() {
    this.navs = [
      { label: "Home", icon: "pi pi-home", routerLink: "/" },
      { label: "Incomes", icon: "pi pi-money-bill", routerLink: "/incomes" },
      { label: "Expenses", icon: "pi pi-chart-pie", routerLink: "/expenses" },
      { label: "Goals", icon: "pi pi-chart-line", routerLink: "/goals" },
      {
        label: "Show", icon: "pi pi-list", items:
          [{ label: 'Start New Budget', icon: 'pi pi-fw pi-refresh' },
          { label: 'Save Current Budget', icon: 'pi pi-fw pi-save' },
          { label: 'Load Saved Budget', icon: 'pi pi-fw pi-download' },
          { label: 'Log Out', icon: 'pi pi-fw pi-sign-out' }]
      }
    ];
  }
}
