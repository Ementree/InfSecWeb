import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './material.module';
import { RsaComponent } from './rsa/rsa.component';
import { EncryptionComponent } from './encryption/encryption.component';
import { ElGamalComponent } from './el-gamal/el-gamal.component';
import { DsaComponent } from './dsa/dsa.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RsaComponent,
    EncryptionComponent,
    ElGamalComponent,
    DsaComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AngularMaterialModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'rsa', component: RsaComponent },
      { path: 'encryption', component: EncryptionComponent },
      { path: 'elgamal', component: ElGamalComponent },
      { path: 'dsa', component: DsaComponent },
    ]),
    NoopAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
