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

import { AppHttpService } from './services/app-http.service';
import { TokenStorageService } from './services/token-storage.service';

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
  providers: [AppHttpService, TokenStorageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
