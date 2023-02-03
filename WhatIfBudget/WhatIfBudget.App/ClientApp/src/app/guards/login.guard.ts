import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {AuthService} from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return new Observable<boolean>((subscriber) => {
      this.authService.isLoggedIn().subscribe((res: boolean) => {
        if(res){
          subscriber.next(true);
        } else {
          sessionStorage.setItem('afterLogin', state.url);
          this.router.navigate(['/login'], {skipLocationChange: true});
          subscriber.next(false);
        }
      });
    });
  }

}
