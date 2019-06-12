import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from 'src/app/komponenta/osoba';
import { Observable } from 'rxjs';

@Injectable()
export class AuthHttpService{

    base_url = "http://localhost:52295"
    constructor(private http: HttpClient){
        
    }

    logIn(username: string, password: string) : Observable<any>{

        return Observable.create((observer) => {

            let data = `username=${username}&password=${password}&grant_type=password`;
            let httpOptions={
                headers:{
                    "Content-type": "application/x-www-form-urlencoded"
                }
            }
            this.http.post<any>(this.base_url + "/oauth/token",data,httpOptions).subscribe(data => {
                console.log("uso u post");
                localStorage.jwt = data.access_token;
                let jwtData = localStorage.jwt.split('.')[1]
                let decodedJwtJsonData = window.atob(jwtData)
                let decodedJwtData = JSON.parse(decodedJwtJsonData)

                let role = decodedJwtData.role
        
                console.log('jwtData: ' + jwtData)
                console.log('decodedJwtJsonData: ' + decodedJwtJsonData)
                console.log('decodedJwtData: ' + decodedJwtData)
                console.log('Role ' + role)
                localStorage.setItem("role", role);
                localStorage.setItem("loggedUser",username);
                observer.next("uspesno");
                observer.complete();
               /* localStorage.jwt = data.access_token;
                observer.next("uspesno");
                localStorage.setItem("loggedUser",username);
                observer.complete();*/
            },
            err => {
                console.log(err);
                observer.next("neuspesno");
                observer.complete();
            });
        });
     
    }

    logOut(): Observable<any>{
        return Observable.create((observer) => {
            localStorage.setItem("loggedUser",undefined);
            localStorage.jwt = undefined;
            localStorage.role = undefined;
        });
    }


    registration(data: User){
        return this.http.post<any>(this.base_url + "/api/Account/Register", data).subscribe();
    }

    GetCenaKarte(tip: string): Observable<any>{
        return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetKarta/" + tip);
    }

    GetKupiKartu(tipKarte: string, tipKorisnika: string, user : string): Observable<any>{
       
        return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetKartaKupi2/" + tipKarte + "/" + tipKorisnika + "/" + user);
    }

    GetAllLines() : Observable<any>{
        return Observable.create((observer) => {
            this.http.get<any>(this.base_url + "/api/Lines/GetLinije").subscribe(data =>{
                observer.next(data);
                observer.complete();
            }) 
        });
    }
    GetTipKorisnika(user : string): Observable<any>{
       
        return this.http.get<any>(this.base_url + "/api/Account/GetTipKorisnika/" + user);
    }
}