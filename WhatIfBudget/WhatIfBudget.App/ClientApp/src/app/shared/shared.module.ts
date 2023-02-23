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


@NgModule({
  declarations: [
    NavigationComponent,
    BudgetListingComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    MenubarModule,
    ButtonModule,
    SplitButtonModule,
    MenuModule,
    TabMenuModule,
    OverlayPanelModule
  ],
  exports: [
    NavigationComponent
  ]
})
export class SharedModule { }
