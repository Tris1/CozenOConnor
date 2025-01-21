import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AttorneyService } from './services/attorney.service';
import { Attorney } from './models/attorney';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  attorneys: Attorney[] = [];
  updatedAttorneys: Attorney[] = [];
  attorneyToEdit?: Attorney;
  isAdminRole?: boolean = false;
  toDisplay: boolean = true;

  constructor(private attorneyService: AttorneyService) { }

  ngOnInit() {
    this.attorneyService.getAttorneys().subscribe((result: Attorney[]) => (this.attorneys = result)
    );
  }

  isAdminRoleHandler(isUserAdmin: boolean) {
    this.isAdminRole = isUserAdmin;
  }

  initNewAttorney() {
    this.attorneyToEdit = new Attorney();
    this.toDisplay = false;
  }

  deleteAttorney(attorney: Attorney) {
    this.attorneyService.deleteAttorney(attorney).subscribe((result: Attorney[]) => (this.attorneys = result));
  }

  updateHeroesList(attorneys: Attorney[]) {
    this.attorneys = attorneys;
  }
}
