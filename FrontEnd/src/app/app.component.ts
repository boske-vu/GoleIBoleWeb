import { Component } from '@angular/core';
import { Osoba } from './komponenta/osoba';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'vezba3test';
  peraOsoba: Osoba = {name:"Pera", surname:"varga"}
}
