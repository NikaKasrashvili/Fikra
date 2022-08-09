export interface User{
    userID: number,
    userFirstName: string,
    userLastName: string,
    userEmail: string,
    jwt: string,
    roleID: number,
    roleName: string,
    userRoleName: string
}