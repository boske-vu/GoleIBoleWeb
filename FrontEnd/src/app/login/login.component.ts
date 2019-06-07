import { Component, OnInit } from '@angular/core';
import { User } from '../komponenta/osoba';
import { AuthHttpService } from '../services/http/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private http: AuthHttpService) { }

  ngOnInit() {
  }

  login(user: User,form: NgForm){
    this.http.logIn(user.username,user.password);
    form.reset();
  }
}
