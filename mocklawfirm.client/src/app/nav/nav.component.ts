import { Component, EventEmitter, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { LoginService } from '../services/login.service';
import { FormGroup, NgForm } from '@angular/forms';
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";

@Component({
  selector: 'app-nav',
  standalone: false,
  
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
  providers: [BsModalService],
})

export class NavComponent implements OnInit {
  @Output() isAdminRole = new EventEmitter<boolean>();


  model: any = {}
  loggedIn: boolean = false;
  isAdmin: boolean = false;
  accessToken?: string;
  userRoleModel: any = {};
  isLoginCredsUnauthorized: boolean = false;
  @ViewChild('loginError') templateref?: string | TemplateRef<any> | any;

  user!: any;
  modalRef?: BsModalRef;

  constructor(private loginService: LoginService, private modalService: BsModalService) { }

  ngOnInit(): void {

  }

  exitModal = (): void => {
    this.modalRef?.hide();
  };

  checkIfUserIsAdmin(value: boolean) {
    this.isAdminRole.emit(value);
  }

  login(form: NgForm) {
    this.loginService.login(this.model).subscribe(response => {
      localStorage.setItem('token', JSON.stringify(response.body?.accessToken));      
      this.loggedIn = true;
      this.chkAdminRole();

      form.reset();

    }, error => {
      if (error.status === 401) {
        this.isLoginCredsUnauthorized = true;
        this.modalRef = this.modalService.show(this.templateref);
      }
    })
  }

  logout() {
    this.loggedIn = false;
    this.isAdmin = false;
    localStorage.removeItem('token');
    this.checkIfUserIsAdmin(this.isAdmin);
  }

  chkAdminRole() {
    this.userRoleModel.email = this.model.email;
    this.loginService.checkUserRole(this.userRoleModel).subscribe(response => {
      if (response) {
        this.isAdmin = true;
        this.checkIfUserIsAdmin(this.isAdmin);
      } else {
        this.isAdmin = false;
      }
    }, error => {
    })
  }
}
