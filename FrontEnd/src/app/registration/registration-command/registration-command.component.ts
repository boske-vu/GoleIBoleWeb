import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/classes/user';
import {Validators} from '@angular/forms';
import {AuthHttpService} from 'src/app/services/http/auth.service';
import { NgForm} from '@angular/forms';
import {Router} from '@angular/router';
import {FormBuilder} from '@angular/forms';
import {FormArray} from '@angular/forms';

@Component({
  selector: 'app-registration-command',
  templateUrl: './registration-command.component.html',
  styleUrls: ['./registration-command.component.css']
})
export class RegistrationCommandComponent implements OnInit {
  registracijaForm = this.fb.group({
    Name: ['', Validators.required],
    Surname: ['',Validators.required],
    Username: ['',Validators.required],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    Email: ['', Validators.required],
    Date: ['', Validators.required],
    Address: ['', Validators.required],
    PhoneNumber: ['', Validators.required],
    imageUrl: ['']
    
    //typeOfUser: ['',Validators.required]
   
  });

  constructor(private http: AuthHttpService, private fb:FormBuilder, private router: Router) { }

  ngOnInit() {
  }

  register(){
    let regModel: User = this.registracijaForm.value;
    this.http.register(regModel);
  }

}
