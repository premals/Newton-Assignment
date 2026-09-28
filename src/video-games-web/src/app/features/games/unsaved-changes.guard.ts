import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { from, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

export interface HasUnsavedChanges {
  hasUnsavedChanges(): boolean;
}

export const unsavedChangesGuard: CanDeactivateFn<HasUnsavedChanges> = component => {
  if (!component.hasUnsavedChanges()) return true;

  const modal = inject(NgbModal).open(ConfirmDialogComponent);
  return from(modal.result as Promise<boolean>).pipe(catchError(() => of(false)));
};
