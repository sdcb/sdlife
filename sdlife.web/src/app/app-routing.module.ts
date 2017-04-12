import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login.component';
import { AccountingComponent } from './components/accounting.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'accounting', component: AccountingComponent },
    { path: '', redirectTo: '/accounting', pathMatch: 'full' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {
        useHash: true
    })],
    exports: [RouterModule],
    providers: []
})
export class AppRoutingModule { }
