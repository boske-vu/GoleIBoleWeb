import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/komponenta/osoba';
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
    firstName: ['', Validators.required],
    lastName: ['',Validators.required],
    username: ['',Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    email: ['', Validators.required],
    date: ['', Validators.required],
    typeOfUser: ['',Validators.required]
  });

  constructor(private http: AuthHttpService, private fb:FormBuilder, private router: Router) { }

  ngOnInit() {
  }

  register(){
    let regModel: User = this.registracijaForm.value;
    this.http.registration(regModel);
  }

}
