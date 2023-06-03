import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public email: string = "";
  public password: string = "";

  constructor(private http: HttpClient) { }

  login() {
    console.log('Login clicked!');
    const model = { Email: this.email, Password: this.password };

    this.http.post('api/Login', model)
      .subscribe((response: any) => {
        if (response.authenticated) {
          console.log("Autenticated");
        } else {
          console.log("Failed");
        }
      });
  }
}
