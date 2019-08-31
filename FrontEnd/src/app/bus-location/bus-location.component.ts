import { Component, OnInit, Input, NgZone } from '@angular/core';
import { GeoLocation } from '../map/map-model/geolocation';
import { MarkerInfo } from '../map/map-model/marker-info.model';
import { Polyline } from '../map/map-model/polyline';
import { raspored, klasaPodaci, linja, LinijaZaHub } from 'src/app/classes/user';
import { BusLocationHttpService } from '../services/busLocation.service';
import { LineHttpService } from '../services/line.service';
import { AuthHttpService } from '../services/http/auth.service';

import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-bus-location',
  templateUrl: './bus-location.component.html',
  styleUrls: ['./bus-location.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}']
})
export class BusLocationComponent implements OnInit {
  isConnected: Boolean;
  locations: string[];
  polasci: string;
  ras: raspored = new raspored();
  linija: linja = new linja();
  klasa: klasaPodaci = new klasaPodaci();
  selectedLine: string;
  linijeZaView: number[];
  dani: string[] = ["Radni", "Subota", "Nedelja"];
  dan: string;
  text: string = "Klisa";
  markeri: MarkerInfo[] = [];
  busKordinate: string[];
  autobusMarker: MarkerInfo;
  public polylineMoje: Polyline;
  promena: boolean = false;

  constructor(private lokacijaServis: BusLocationHttpService,private httpAuth: AuthHttpService, private http: LineHttpService, private ngZone: NgZone, private router: Router) {
    this.isConnected = false;
    this.locations = [];

  }

  ngOnInit() {
    this.promena = false;
    this.polylineMoje = new Polyline([], 'blue', { url: "assets/lasta.jpg", scaledSize: { width: 50, height: 50 } });
    this.http.GetLinije().subscribe((linijesabekenda) => {
      this.linijeZaView = linijesabekenda;
      err => console.log(err);
    });
    this.subscribeForLocations();
    this.checkConnection();

    //this.lokacijaServis.registerForLocation();
  }

  private checkConnection() {
    this.lokacijaServis.startConnection().subscribe(e => {
    this.isConnected = e;
      console.log(e);
      if (e) {
        //this.lokacijaServis.StartTimer();
      }
    });
  }

  private subscribeForLocations() {
    this.lokacijaServis.registerForLocation().subscribe(l => this.onNotification(l));
  }

  public onNotification(notification: number[]) {

    this.ngZone.run(() => {
      console.log(notification);
      if(this.promena){

      
      //let busevi = notification.split(";");
      //busevi.forEach(element => {
      //let temp = busevi[0].split("_");
      //if(temp[0] == this.selectedLinija)
      let kord1 = notification[0];
      let kord2 = notification[1];
      //this.busKordinate = temp;
      if (kord1 != undefined && kord2 != undefined) {
        //var x = parseFloat(this.busKordinate[1].replace(',', '.'));
        //var y = parseFloat(this.busKordinate[0].replace(',', '.'));

        this.autobusMarker = new MarkerInfo(new GeoLocation(kord2, kord1), "assets/lasta.jpg", "", "", "");
        this.polylineMoje.addLocation(new GeoLocation(+kord2, +kord1), true);
        this.markeri.push(this.autobusMarker);
      }
    }
    });
  }

  onSelectionChangeNumber(event) {
    this.promena = true;
    //this.stations = [];
    this.polylineMoje.path = [];
    if (event.target.value == "") {
      this.promena = false;
      //this.stations = [];
      this.polylineMoje.path = [];
      this.stopTimer();
    } else {
      this.checkConnection();
      this.subscribeForLocations();
      //this.stopTimer();
      //this.getStationsByLineNumber(event.target.value); 
      this.stopTimer();
      var lin = new LinijaZaHub(event.target.value);
      this.httpAuth.StanicaZaHub(lin).subscribe();
      //this.lokacijaServis.notificationReceived.subscribe(l => this.onNotification(l));
      //  this.notifForBL.StartTimer(); 
    }

  }

  OnGetPolasci() {
    this.lokacijaServis.StartTimer();
    this.lokacijaServis.notificationReceived.subscribe(l => this.onNotification(l));
    //this.polylineMoje.addLocation(new GeoLocation(+this.busKordinate[1], +this.busKordinate[0]));
  }

  stopTimer() {
    this.lokacijaServis.StopTimer();
  }

  public startTimer() {
    this.lokacijaServis.StartTimer();
  }

  Stop(){
    this.lokacijaServis.StopTimer();
    this.polylineMoje = new Polyline([], 'blue', null);
    this.markeri = [];
  }

  

}

