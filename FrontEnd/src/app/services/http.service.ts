import { Injectable } from '@angular/core';
import {Observable, of} from 'rxjs';

@Injectable()
export class HttpService{
    getName(): Observable<string>{
        return of("Neko Ime iz servisa");
    }
}