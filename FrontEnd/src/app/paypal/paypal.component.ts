import { Component, OnInit,AfterViewChecked, Input } from '@angular/core';
import { AuthHttpService } from '../services/http/auth.service';
import { CenovnikHttpService } from '../services/cenovnik.service';
import { UserType } from '../classes/userType';

declare let paypal: any;

@Component({
  selector: 'app-paypal',
  templateUrl: './paypal.component.html',
  styleUrls: ['./paypal.component.css']
})
export class PaypalComponent implements OnInit {

  @Input() 
  cenaKarte: string;

  constructor(private http: AuthHttpService,private http1: CenovnikHttpService) { }
  cena: number = 0;

  ngOnInit() {
    // this.http1.getCene(this.valuePrice,this.valueUser).subscribe((data) => {
    //   // this.cena1 = this.cena.toString();
    //   // console.log("ovo je cena: " + this.cena1);
    //   console.log("vrednosti " + this.valuePrice + "druga: " + this.valueUser);
    //   err => console.log(err);
    // });
    console.log("ngOninit paypal" + this.cenaKarte);
    this.cena = parseFloat(this.cenaKarte);
    console.log("ngOninit paypal" + this.cenaKarte);
  }
  

  private loadExternalScript(scriptUrl: string) {
    return new Promise((resolve, reject) => {
      const scriptElement = document.createElement('script')
      scriptElement.src = scriptUrl
      scriptElement.onload = resolve
      document.body.appendChild(scriptElement)
  })}

  


  ngAfterViewInit(): void {
    var c = this.cena;
    var v = this.http;
    this.loadExternalScript("https://www.paypalobjects.com/api/checkout.js").then(() => {
      paypal.Button.render({
        env: 'sandbox',
        client: {
          production: 'sb-il7hy120462@business.example.com',
          sandbox: 'AZXC4ZzNSm5Eabby-0FIOO_Y40nNSsPHbC5RVm9gkEE6p5qke3oFc4BUKECEK909sgHVLlZFmzC8TshI',
          
        },
        commit: true,
        payment: function (data, actions) {
          return actions.payment.create({
            payment: {
              transactions: [
                {
                  amount: { total: c, currency: 'USD' }
                }
              ]
            }
          })
        },
        onAuthorize: function(data, actions) {
          return actions.payment.execute().then(function(payment) {
            // TODO
            var l = v.SacuvajTransakciju(payment.id).subscribe();
          })
        }
      }, '#paypal-button');
    });
  }
 
}