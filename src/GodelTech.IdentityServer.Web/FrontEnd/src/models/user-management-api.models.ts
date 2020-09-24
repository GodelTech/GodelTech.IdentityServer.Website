export class UserProfile {
    public id: string;
    public email: string;
    public name: string;
    public userName: string;
}

// tslint:disable-next-line:max-classes-per-file
export class ListOf<T> {
    public total: number;
    public collection: T[];
}

// tslint:disable-next-line:max-classes-per-file
export class ListOfUsers {
    public total: number;
    public collection: UserProfile[];
}

export interface IAuthenticateSuccessResponse {
    id: string;
    auth_token: string;
    expires_in: number;
}

export interface IUser {
    id: string;
    email: string;
}

export interface IAuthenticateErrorResponse {
    statusCode: number;
    message: string;
    login_failure: string;
    Password: string[];
    UserName: string[];
}

export interface IRegisterErrorResponse {
    statusCode: number;
    message: string;
}

export interface IResetPasswordErrorResponse {
    statusCode: number;
    message: string;
}

export interface IRegisterSuccessResponse {
    statusCode: number;
    message: string;
}
