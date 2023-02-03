import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { LoginBlockComponent } from './components/login-block/login-block.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';

@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    LoginBlockComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,

    CardModule,
    ButtonModule
  ]
})
export class HomeModule { }
