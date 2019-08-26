import { Component, OnInit,AfterViewChecked, Input } from '@angular/core';

declare let paypal: any;

@Component({
  selector: 'app-paypal',
  templateUrl: './paypal.component.html',
  styleUrls: ['./paypal.component.css']
})
export class PaypalComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  private loadExternalScript(scriptUrl: string) {
    return new Promise((resolve, reject) => {
      const scriptElement = document.createElement('script')
      scriptElement.src = scriptUrl
      scriptElement.onload = resolve
      document.body.appendChild(scriptElement)
  })}

  ngAfterViewInit(): void {
    this.loadExternalScript("https://www.paypalobjects.com/api/checkout.js").then(() => {
      paypal.Button.render({
        env: 'sandbox',
        client: {
          production: 'goran.pavicevic.96-facilitator@gmail.com',
          sandbox: 'AYzNBVp9wSbvxR_BVTKgq5HPkTIMY8gV5ON31ANKMYc-DgqVk3GLGwwK9LnJeHDGYTeqMvJGOm40L27A',
          
        },
        commit: true,
        payment: function (data, actions) {
          return actions.payment.create({
            payment: {
              transactions: [
                {
                  amount: { total: '1.00', currency: 'USD' }
                }
              ]
            }
          })
        },
        onAuthorize: function(data, actions) {
          return actions.payment.execute().then(function(payment) {
            // TODO
          })
        }
      }, '#paypal-button');
    });
  }
 
}