import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { AddStation } from '../classes/addStation';
import { LinijaZaHub } from 'src/app/classes/user';
import { HttpClient } from '@angular/common/http';

declare var $: any;

@Injectable()
export class BusLocationHttpService {

  base_url = "http://localhost:52295"

  private proxy: any;  
  private proxyName: string = 'notifications';  
  private connection: any;  
  public connectionExists: Boolean; 

  public notificationReceived: EventEmitter < string >;  

  constructor(private http: HttpClient) {  
      this.notificationReceived = new EventEmitter<string>();
      this.connectionExists = false;  
      // create a hub connection  
      this.connection = $.hubConnection("http://localhost:52295/");
      this.connection.qs = { "token" : "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiJ9.eyJuYW1laWQiOiJhZG1pbiIsInVuaXF1ZV9uYW1lIjoiYWRtaW5AeWFob28uY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS9hY2Nlc3Njb250cm9sc2VydmljZS8yMDEwLzA3L2NsYWltcy9pZGVudGl0eXByb3ZpZGVyIjoiQVNQLk5FVCBJZGVudGl0eSIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiZTA2YzY2YTgtZWMwNC00M2UwLTgzNTYtZTAzZjY0MDNjNmM0Iiwicm9sZSI6IkFkbWluIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjI5NSIsImF1ZCI6ImJXbHNZWE5vYVc0PSIsImV4cCI6MTU1OTU3MzIxMCwibmJmIjoxNTU5NDg2ODEwfQ.uGs1m19mRCf-6ZETmiRGpuhSHgp2eeOHNh0kCxAS2oV5lFQIJjXQPM-QlCyBM9g9irODOmsNTKQXnWLHTqMngA" };
      // create new proxy with the given name 
      this.proxy = this.connection.createHubProxy(this.proxyName);  
      
  }  
 
  // browser console will display whether the connection was successful    
  public startConnection(): Observable<Boolean> { 
      
    return Observable.create((observer) => {
       
        this.connection.start()
        .done((data: any) => {  
            console.log('Now connected ' + data.transport.name + ', connection ID= ' + data.id)
            this.connectionExists = true;

            observer.next(true);
            observer.complete();
        })
        .fail((error: any) => {  
            console.log('Could not connect ' + error);
            this.connectionExists = false;

            observer.next(false);
            observer.complete(); 
        });  
      });
  }

  public registerForLocation(): Observable<number[]> {  
      
    return Observable.create((observer) =>{

      this.proxy.on('getBusData', (data: number[]) => {  
          console.log('received notification: ' + data);  
          observer.next(data);
          //this.notificationReceived.emit(data);  
      }); 
    });
  }  


  public StopTimer() {
      this.proxy.invoke("StopLocationServerUpdates");
  }

  public StartTimer() {
      this.proxy.invoke("StartLocationServerUpdates");
  }

  
}