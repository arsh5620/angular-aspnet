import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from '../member-card/member-card.component';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  http = inject(HttpClient);
  membersService = inject(MembersService)
  members: Member[] = [];

  ngOnInit(): void {
    this.getMembers();
  }

  getMembers() {
    this.membersService.getMembers().subscribe(
      {
        next: response => this.members = response
      }
    )
  }
}
