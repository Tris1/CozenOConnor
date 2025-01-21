import { Component, EventEmitter, Input, Output, TemplateRef, ViewChild } from '@angular/core';
import { Attorney } from '../../models/attorney';
import { AttorneyService } from '../../services/attorney.service';
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";

@Component({
  selector: 'app-edit-attorney',
  standalone: false,
  
  templateUrl: './edit-attorney.component.html',
  styleUrl: './edit-attorney.component.css',
  providers: [BsModalService],
})

export class EditAttorneyComponent {
  @Input() attorney?: Attorney;
  @Output() attorneysUpdated = new EventEmitter<Attorney[]>();
  @ViewChild('newAttorneyName') attorneyNameInput: any; 
  @ViewChild('newAttorneyEmail') attorneyNameEmailInput: any; 
  @ViewChild('newAttorneyPhoneNumber') attorneyPhoneNumberInput: any;
  @ViewChild('createNewAttorneyError') templateref?: string | TemplateRef<any> | any;

  user!: any;
  modalRef?: BsModalRef;

  constructor(private attorneyService: AttorneyService, private modalService: BsModalService) { }

  updateAttorney(attorney: Attorney) { }

  deleteAttorney(attorney: Attorney) {
    this.attorneyService
      .deleteAttorney(attorney)
      .subscribe((attorneys: Attorney[]) => this.attorneysUpdated.emit(attorneys));
  }

  createAttorney(attorney: Attorney) {
    this.attorneyNameInput.nativeElement.value = '';
    this.attorneyNameEmailInput.nativeElement.value = '';
    this.attorneyPhoneNumberInput.nativeElement.value = '';
    this.attorneyService
      .addAttorney(attorney)
      .subscribe((attorneys: Attorney[]) => this.attorneysUpdated.emit(attorneys), error => {
        if (error.status === 400) {
          this.modalRef = this.modalService.show(this.templateref);
        }
      });
  }
}
