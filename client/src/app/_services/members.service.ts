import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})

export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = []; //keep instantiation 
  memberCache = new Map(); //Map for caching
  user: User;
  userParams: UserParams;

  constructor(private http: HttpClient, private accountService: AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(); //pass user, but for now giving error ?
    })
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  //the following method might not be necessary. check later
  resetUserParams() {
    this.userParams = new UserParams(); //takes this.user but for now error
    return this.userParams;
  }

  getMembers(userParams: UserParams) {
    //CACHING responses so it doesn't "request" the data every time
    var response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('orderBy', userParams.orderBy);
  
    //the mapping in this method is for CACHING
    return this.getPaginatedResult<Member[]>(this.baseUrl + 'users', params)
      .pipe(map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      }))
  }

  getMember(username: string) {
    //[...] is spread operator
    const member = [...this.memberCache.values()]
    .reduce((arr, elem) => arr.concat(elem.result), [])
    .find((member: Member) => member.username === username);

    if (member) {
      return of(member);
    }
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  } 

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }

  addFollow(username: string) {
    //'post' always needs to send an empty body at the end, hence {}
    return this.http.post(this.baseUrl + 'following/' + username, {});
  }

  getFollowing(predicate: string, pageNumber, pageSize) {
    let params = this.getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate);
    return this.getPaginatedResult<Partial<Member[]>>(this.baseUrl + 'following', params);
  }

  private getPaginatedResult<T>(url, params) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());

      return params;

  }

}
