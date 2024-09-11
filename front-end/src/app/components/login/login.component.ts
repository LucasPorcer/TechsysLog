import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/login/loginModel';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  credentials = { username: '', password: '' };
  loginError = false;

  constructor(private authService: ApiService, private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('jwt');
    if (token) {
      this.router.navigate(['/pedidos']);
    }
  }

  onSubmit() {
    const loginRequest = new LoginModel();
    loginRequest.email = this.credentials.username;
    loginRequest.password = this.credentials.password;

    this.authService.auth(loginRequest).subscribe({
      next: (response) => {
        localStorage.setItem('jwt', response.jwt);
        this.router.navigate(['/pedidos']);
      },
      error: () => {
        this.loginError = true;
      }
    });
  }
}
