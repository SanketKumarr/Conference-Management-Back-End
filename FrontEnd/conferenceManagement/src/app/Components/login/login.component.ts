import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/Model/Login/login';
import { User } from 'src/app/Model/User/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  login: Login

  constructor(private http: HttpClient, private router: Router) {
    this.login = new Login()
  }

  Login() {
    this.http.post<User>("https://localhost:44325/api/Login/Login", this.login).subscribe(result => {
      console.warn(result)
      if (result) {
        this.router.navigate([])
      }
    })
  }

}
