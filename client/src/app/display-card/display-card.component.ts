import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-display-card',
  templateUrl: './display-card.component.html',
  styleUrls: ['./display-card.component.css']
})
export class DisplayCardComponent implements OnInit {
  @Input() member: Member;

  constructor(private memberService: MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    
  }

}
