import { Component, OnInit } from '@angular/core';
import { CenovnikHttpService } from '../services/cenovnik.service';
import { KupiKartu } from '../classes/kupiKartu';
import { ProfilHttpService } from '../services/profil.service';
import { User } from '../classes/user';
import { NumberSymbol } from '@angular/common';

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements OnInit {

  constructor(private http: CenovnikHttpService, private http2: ProfilHttpService) { }
  karta: KupiKartu = new KupiKartu();
  userType: string
  cena: number
  karte: string[] = []
  selectedTip: string
  kupljena: string
  cena1: string = ''
  userNameLogovanog: string
  user: User = new User();
  verificateNumber: number

  ngOnInit() {
    
    this.karte = [];
    this.kupljena = "";
    this.http.getUserType().subscribe((userType) => {
      
      this.userType = userType;
      localStorage.userType = userType;
      console.log(this.userType);
      if(this.userType == "neregistrovan")
      {
        
        this.karte.push("Vremenska karta");
      }
      else
      {
        console.log("this: " + this.userType);
        console.log("ret: " + userType);
        this.karte.push("Vremenska karta");
        this.karte.push("Dnevna karta");
        this.karte.push("Mesečna karta");
        this.karte.push("Godišnja karta");         
      }
      this.selectedTip = this.karte[0];
      
      err => console.log(err);
    });   

    this.http2.getUser().subscribe((data) => {
      this.user = data;
      this.verificateNumber = data.VerificateAcc;
      console.log("broj " + this.verificateNumber);
    });

  }

  getCenaKarte(){
    console.log("selektovana karta44: " + this.selectedTip);
    this.http.getCene(this.selectedTip,this.userType).subscribe((data) => {
      this.cena = data;
      this.cena1 = this.cena.toString();
      console.log("ovo je cena: " + this.cena1);
      err => console.log(err);
    });
  }
  
  kupiKartu(){
    console.log("selektovana karta2: " + this.selectedTip);

    

    this.karta.Price = this.cena;

    this.karta.Username = localStorage.loggedUser;
    this.karta.TipKarte = this.selectedTip;
    
   
    if (this.verificateNumber == 1) {
      this.http.kupiKartu(this.karta).subscribe((data) => {
        this.cena1 = data;
        if(data == "uspesno")
        {
          this.kupljena = "uspesno";
        }
        else
        {
          this.kupljena = "doslo je do greske prilikom kupovine karte.";
        }
        err => console.log(err);
      });
    } 
    else{
      alert("Verifikujte nalog kako bi mogli kupiti kartu");
      console.log("brojKAsnije " + this.verificateNumber);
    }
   
  }

}
