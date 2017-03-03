import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AccountingComponent } from './accounting/accounting.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent }, 
  { path: 'accounting', component: AccountingComponent }, 
  { path: '', component: AccountingComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    useHash: true
  })],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
