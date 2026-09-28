import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirm-dialog',
  template: `
    <div class="modal-header">
      <h5 class="modal-title">Unsaved changes</h5>
    </div>
    <div class="modal-body">You have unsaved changes. Leave anyway?</div>
    <div class="modal-footer">
      <button type="button" class="btn btn-outline-secondary" (click)="activeModal.close(false)">Stay</button>
      <button type="button" class="btn btn-danger" (click)="activeModal.close(true)">Leave</button>
    </div>
  `,
})
export class ConfirmDialogComponent {
  constructor(public activeModal: NgbActiveModal) {}
}
