import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class MapHttpService{
    base_url = "http://localhost:52295"
    constructor(private http: HttpClient){ }

    // GetStanicaCord(idStanice: string): Observable<any> {
    //     return this.http.get<any>(this.base_url + "/api/LineEdit/GetStanica" + idStanice)
    // }

    GetStanicaCord(idStanice: string): Observable<any> {
        return Observable.create((observer) => {    
            this.http.get<any>(this.base_url + "/api/LineEdit/GetStanica/" + idStanice).subscribe(data =>{
                observer.next(data);
                observer.complete();     
            })             
        });
    }
}