import { ActivatedRouteSnapshot, CanActivate, ResolveEnd, Router, RouterStateSnapshot } from "@angular/router";
import { FirebaseAuthService } from "./firebaseAuth/firebase-auth.service";
import { Observable,map } from "rxjs";
import { resolve } from "dns";
import jwt_decode, { JwtPayload } from 'jwt-decode';
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
export class AuthGuard implements CanActivate 
{
    private _authService:FirebaseAuthService
    private _router:Router;

     constructor(authService: FirebaseAuthService, router: Router)
     {
        this._authService = authService;
        this._router = router;
     }

     canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): 
     Observable<boolean> | Promise<boolean> | boolean 
     {
       if(!this._authService.isAuthenticated())
       {
        return false;
       }
       let token = this._authService.getToken();
       let decodedToken:any = jwt_decode(token!);
       let role:string = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
       let inputRoles:string[] = next.data['roles'];
       if(inputRoles.includes(role))
       {
        return true;
       }
       return false;
     }
}
