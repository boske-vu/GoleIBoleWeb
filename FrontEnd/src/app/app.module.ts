import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { RouterModule,Routes} from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AuthHttpService } from './services/http/auth.service';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { UiModule } from './ui/ui.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RedvoznjeComponent } from './redvoznje/redvoznje.component';
//import { RedVoznjeHttpService } from 'src/services/redvoznje.service';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { CenovnikHttpService } from './services/cenovnik.service';
import { ProfilComponent } from './profil/profil.component';
import { ProfilHttpService } from './services/profil.service';
import { CardVerificationComponent } from './card-verification/card-verification.component';
import { CardVerificationHttpService } from './services/cardVerification.service';
import { RedVoznjeHttpService } from './services/redvoznje.service';
import { AgmCoreModule } from '@agm/core';
import { BusLocationHttpService } from './services/busLocation.service';
import { from } from 'rxjs';
import { AuthGuardAdmin } from './services/http/auth.guard';
import { AuthGuardController } from './services/http/auth2.guard';
import { VerificateUserComponent } from './verificate-user/verificate-user.component';
import { VerificateUserHttpService } from './services/verificateUser.service';
import { LineComponent } from './line/line.component';
import { LineHttpService } from './services/line.service';
import { Line } from './classes/line';
import { StationEditComponent } from './station-edit/station-edit.component';
import { StationEditHttpService } from './services/stationEdit.service';
import { PaypalComponent } from './paypal/paypal.component';
import { PriceListEditComponent } from './price-list-edit/price-list-edit.component';
import { PriceListEditHttpService } from './services/priceListEdit.service';
import { MapComponent } from './map/map.component';
import {MapHttpService} from './services/map.service';
import { BusLocationComponent } from './bus-location/bus-location.component';


const routes : Routes = [
  {path : "login", component: LoginComponent},
  {path : "home", component: HomeComponent},
  {path : "register", component: RegisterComponent},
  {path : "redvoznje", component: RedvoznjeComponent},
  {path : "cenovnik", component: CenovnikComponent},
  {path : "profil", component: ProfilComponent},
  {path: "cardVerification", component: CardVerificationComponent},
  {path: "verificateUser", component: VerificateUserComponent},
  {path: "line", component: LineComponent},
  {path: "stationEdit", component: StationEditComponent},
  {path: "priceListEdit", component: PriceListEditComponent},
  {path: "map", component: MapComponent},
  {path: "location", component: BusLocationComponent},
  {path : "", component: HomeComponent, pathMatch:"full"},
  
  {path : "**", redirectTo: ""},
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    RedvoznjeComponent,
    CardVerificationComponent,
    CenovnikComponent,
    ProfilComponent,
    VerificateUserComponent,
    LineComponent,
    StationEditComponent,
    PaypalComponent,
    PriceListEditComponent,
    MapComponent,
    BusLocationComponent
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),  
    ReactiveFormsModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'}),
    UiModule 
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass:TokenInterceptor, multi:true},AuthHttpService,CardVerificationHttpService, CenovnikHttpService,ProfilHttpService,RedVoznjeHttpService, VerificateUserHttpService, LineHttpService, StationEditHttpService, PriceListEditHttpService, MapHttpService, BusLocationHttpService], //svi mogu da pristupe(injektuju servis)
  bootstrap: [AppComponent]
})
export class AppModule { }
