import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  if (component.memberForm?.dirty) {
    return confirm("Any unsaved changes will be lost, continue?");
  }

  return true;
};
