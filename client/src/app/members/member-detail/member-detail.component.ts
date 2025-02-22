import { Component, inject, input, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { ActivatedRoute } from '@angular/router';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [TabsModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit {
  route = inject(ActivatedRoute);
  memberService = inject(MembersService);
  member?: Member;

  ngOnInit(): void {
    var username = this.route.snapshot.paramMap.get('username');
    if (username) {
      this.memberService.getMember(username).subscribe({
        next: response => this.member = response
      })
    }
  }
}
