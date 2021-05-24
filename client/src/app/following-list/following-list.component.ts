import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-following-list',
  templateUrl: './following-list.component.html',
  styleUrls: ['./following-list.component.css']
})
export class FollowingListComponent implements OnInit {
  members: Partial<Member[]>;
  predicate = 'followed';
  pageNumber =1;
  pageSize = 6;
  pagination: Pagination;

  constructor(private memberService: MembersService) { }

  //always use ngOnInit to actually display/run the method
  ngOnInit(): void {
    this.loadFollowing();
  }

  loadFollowing() {
    this.memberService.getFollowing(this.predicate, this.pageNumber, this.pageSize)
      .subscribe(response => {
        this.members = response.result;
        this.pagination = response.pagination;
      })
  }
  
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadFollowing();
  }

}
