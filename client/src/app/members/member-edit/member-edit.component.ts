import { Component, HostListener, inject, OnInit, ViewChild, viewChild } from '@angular/core';
import { Member } from '../../_models/member';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { AccountsService } from '../../_services/accounts.service';
import { MembersService } from '../../_services/members.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  @ViewChild("defaultForm") memberForm?: NgForm;
  @HostListener("window:beforeunload", ["$event"]) notify($event: any) {
    if (this.memberForm?.dirty) {
      $event.returnValue = true;
    }
  }

  member?: Member;
  accountsService = inject(AccountsService);
  memberService = inject(MembersService);
  toastrService = inject(ToastrService);

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    var currentUser = this.accountsService.currentUser();
    if (!currentUser) return;
    this.memberService.getMember(currentUser.username).subscribe(
      {
        next: user => this.member = user
      }
    );
  }

  updateProfile() {
    if (!this.member) {
      console.log("An unexpected error occurred! this.member is null!");
      return;
    }

    this.memberService.updateMember(this.member).subscribe({
      next: _ => {
        this.toastrService.success("Saved successfully");
        this.memberForm?.reset(this.member)
      },
      error: _ => this.toastrService.error("Failed to save the changes")
    });
  }
}
