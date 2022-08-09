export interface UserByEmailModel {
    userID: number,
    userFirstname:string,
    userLastname:string,
    userEmail:string,
    userRoleID: number,
    userDateCreated: Date,
    userIsBanned: boolean,
    userIsEnabled: boolean,
    userImageBase64:string,
}