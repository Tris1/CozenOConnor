import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpResponse } from '../models/HttpResponse';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<HttpResponse>('login', model, { observe: 'response' })
  }

  checkUserRole(userRoleModel: any) {
    console.log(userRoleModel);

    const bodyData = { 'email': userRoleModel.email };

    return this.http.post('api/UserRoles/isAdminRole', bodyData)
  }
}
