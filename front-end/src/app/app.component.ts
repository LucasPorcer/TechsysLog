import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav'; // Importar o MatSidenav
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild('drawer') drawer!: MatSidenav;  // Utilizar o @ViewChild para acessar o drawer
  isHandset$: Observable<boolean>;
  drawerOpened = false;
  isLoginPage = false;

  constructor(private breakpointObserver: BreakpointObserver, private router: Router) {
    this.isHandset$ = this.breakpointObserver.observe(Breakpoints.Handset)
      .pipe(
        map(result => result.matches),
        shareReplay()
      );

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isLoginPage = this.router.url === '/login';
      }
    });
  }

  ngOnInit() {
    this.drawerOpened = false;
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
