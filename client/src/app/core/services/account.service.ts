import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Address, User } from '../../shared/models/user';
import { environment } from '../../../environments/environment';
import { SnackbarService } from './snackbar.service';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  baseURL = environment.ApiUrl;

  http = inject(HttpClient);
  snackbarservice = inject(SnackbarService);

  currentUser = signal<User | null>(null);

  login(values: any) {
    let params = new HttpParams();
    params = params.append('useCookies', true);

    return this.http.post<User>(this.baseURL + '/login', values, { params })
  }

  register(values: any) {
    return this.http.post(this.baseURL + '/account/register', values);
  }

  getUserInfo() {
    console.log("get user info called");
    return this.http.get<User>(this.baseURL + '/account/user-info').pipe(
      map((value: User) => {
        console.log(value);
        this.currentUser.set(value);
        console.log(this.currentUser());
        return value;
      })
    )
  }

  isAuthenticated() {
    return this.http.get<{ isAuthenticated: boolean }>(this.baseURL + '/account/AuthStatus')
  }

  logout() {
    return this.http.get(this.baseURL + '/account/logout');
  }

  updateAddress(value: Address) {
    this.http.post<Address>(this.baseURL + '/account/address', value)
  }



}
