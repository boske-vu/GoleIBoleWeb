import { Component, OnInit } from '@angular/core';
import { User } from '../komponenta/osoba';
import { AuthHttpService } from '../services/http/auth.service';
import { NgForm ,FormBuilder, Validators} from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private http:AuthHttpService, private router: Router,private fb: FormBuilder) { }

  ngOnInit() {
  }

  login(){
    let user: User = this.loginForm.value;
    this.http.logIn(user.username,user.password).subscribe(temp => {
      if(temp == "uspesno")
      {
        console.log(temp);
        this.router.navigate(["/home"])
      }
      else if(temp == "neuspesno")
      {
        console.log(temp);
        this.router.navigate(["/login"])
      }
    });
  }
}