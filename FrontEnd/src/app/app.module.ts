import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {RouterModule, Routes} from '@angular/router';
import { KomponentaComponent } from './komponenta/komponenta.component';
import { HttpService } from './services/http.service';
import { importType } from '@angular/compiler/src/output/output_ast';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthHttpService } from './services/http/auth.service';
import {ReactiveFormsModule} from '@angular/forms'
import { RegistrationCommandComponent } from './registration/registration-command/registration-command.component';
import { from } from 'rxjs';
import { LinkoviComponent } from './linkovi/linkovi.component';
import { BusLinesComponent } from './bus-lines/bus-lines.component';
import { RedvoznjeComponent } from './redvoznje/redvoznje.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import {CenovnikHttpService} from './services/cenovnik.service';


const routes : Routes = [
  {path:"home",component: HomeComponent},
  {path:"login",component: LoginComponent},
  {path: "bus-lines", component: BusLinesComponent},
  {path:"registration",component: RegistrationCommandComponent},
  {path:"redvoznje", component: RedvoznjeComponent},
  {path:"cenovnik", component: CenovnikComponent},
  {path:"",component:HomeComponent,pathMatch:"full"},
  {path:"**",redirectTo:"login"}
]


@NgModule({
  declarations: [
    AppComponent,
    KomponentaComponent,
    HomeComponent,
    LoginComponent,
    RegistrationCommandComponent,
    LinkoviComponent,
    BusLinesComponent,
    RedvoznjeComponent,
    CenovnikComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule
  ],
  providers: [HttpService, {provide: HTTP_INTERCEPTORS, useClass:TokenInterceptor, multi:true},CenovnikHttpService, AuthHttpService], //svi mogu da pristupe(injektuju servis)
  bootstrap: [AppComponent]
})
export class AppModule { }
