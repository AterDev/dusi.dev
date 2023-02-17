import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private auth: LoginService,
  ) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | UrlTree {
    const url = state.url;

    const publicRoutes = ['/blog', '/index', '/news', '/system/login'];

    if (this.auth.isAdmin) {
      return true;
    } else {

      for (const route of publicRoutes) {
        if (url.startsWith(route)) {
          return true;
        }
      }

      if (url.startsWith('/system')) {
        return false;
      }

      if (this.auth.isLogin) {
        return true;
      }
      return this.router.parseUrl('/index');
    }
  }
  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | UrlTree {
    return this.canActivate(next, state);
  }
}
