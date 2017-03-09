import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login.component';
import { AccountingComponent } from './components/accounting.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AccountingComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule, 
    MaterialModule.forRoot(), 
    FlexLayoutModule, 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
