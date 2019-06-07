import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from '../services/http.service';
import { Osoba } from './osoba';
import { ValuesHttpService } from '../services/http/values-http.service';
import { AuthHttpService } from '../services/http/auth.service';

@Component({
  selector: 'app-komponenta',
  templateUrl: './komponenta.component.html',
  styleUrls: ['./komponenta.component.css'],
  providers: [ValuesHttpService, AuthHttpService]
})

export class KomponentaComponent implements OnInit {

  name: string
  clicks: number
  unos:string
  values: string[]

  @Input()
    osoba:Osoba
  
  
  constructor(private http:ValuesHttpService, private auth: AuthHttpService) { } //u konstruktoru mogu da dodam da li je priv/public...

  ngOnInit() {
   //this.http.getName().subscribe((name) => this.name = name);
   this.name = "Saloa";

   this.auth.logIn("admin@yahoo.com", "Admin123!");
   this.http.getAll().subscribe((values) => this.values = values, err => console.log(err));
   this.clicks = 0;
   
  }

  clickCounter(){
    this.clicks++;
  }



}
