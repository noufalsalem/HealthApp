export class UserParams {
    gender: string;
    minAge = 15;
    maxAge = 99;
    pageNumber = 1;
    pageSize = 6;
    orderBy = 'lastActive';

    constructor() {
    }
}