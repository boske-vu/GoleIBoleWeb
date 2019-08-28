import { Component, OnInit, Input, NgZone, Injectable } from '@angular/core';
import { GeoLocation } from './map-model/geolocation';
import { Polyline } from './map-model/polyline';
import { MarkerInfo } from './map-model/marker-info.model';
import { MapHttpService } from '../services/map.service';
import { LineHttpService } from '../services/line.service';
// import { AuthHttpService } from '../services/auth.service';


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] //postavljamo sirinu i visinu mape
})

export class MapComponent implements OnInit {

  @Input()
  linija: string;
  x: number;
  y: number;
  mrakerInfoMoj: MarkerInfo;
  markeri: MarkerInfo[];
  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  public polylineMoje: Polyline;
  public nizKordinata: Cord[];
  selectedLine: string
  selectedStation: string
  lines: string[] = []

  ngOnInit() {

    this.http2.getAll().subscribe((line) => {
      this.lines = line;
      this.selectedLine = this.lines[2];
      err => console.log(err);
    });

    this.mrakerInfoMoj = new MarkerInfo(new GeoLocation(this.x, this.y), 
      "assets/busicon.png",
      "Stanica" , "" , "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka"); 
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954), 
       "assets/ftn.png",
       "Jugodrvo" , "" , "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");

    this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
    this.polylineMoje = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
  }

  constructor(private ngZone: NgZone,private http: MapHttpService, private http2: LineHttpService ){
  }

  placeMarker($event){
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    //console.log(this.polyline)
  }

  placeMarkerLinije(x, y){
    this.polylineMoje.addLocation(new GeoLocation(x, y))
    
    console.log(this.polylineMoje)
  }

  ShowStanica(selectedLine: string){
    selectedLine = this.selectedLine;
    this.http.GetStanicaCord(selectedLine).subscribe((raspored1)=>{
      this.nizKordinata = raspored1;
      err => console.log(err);
    }
    );
    
    // for(var i = 0; i <= this.nizKordinata.length; i++){
    //   var g = new GeoLocation(0,0);
    //   g.latitude = this.nizKordinata[i][0];
    //   g.longitude = this.nizKordinata[i][1];
    //   console.log(this.nizKordinata)
    //   var g = new Cord();
    //   g = this.nizKordinata[i];
      console.log(this.nizKordinata);
      this.nizKordinata.forEach(station => {
        let stationToPush: Cord = {
          x: station.x,
          y: station.y,
          name: station.name,
        };
        this.placeMarkerLinije(station.y, station.x);
    });
  }
    
  HideStanica(){
    this.nizKordinata = [];
    // this.mrakerInfoMoj = new MarkerInfo(new GeoLocation(45.242268, 19.842954),
    //   "assets/ftn.png",
    //   "Jugodrvo", "", "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");
    this.polyline = new Polyline([], 'blue', null);
    this.polylineMoje = new Polyline([], 'blue', null);
    //this.placeMarkerLinije(45.242268, 19.842954); 
  }
  
}

class Cord{
  x: number;
  y: number;
  name: string;
}
