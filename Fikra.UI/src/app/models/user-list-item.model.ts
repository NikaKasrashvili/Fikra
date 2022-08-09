export interface UserListItemModel {
    userID? : number,
    userFirstname : string,
    userLastname : string,
    userEmail : string,
    userRoleID? : number,
    userDateCreated : Date,
    userIsBanned : boolean,
    userIsEnabled : boolean,
    userRoleName: string
}