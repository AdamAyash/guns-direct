import { callbackify } from "util";
import { JwtModel } from "../../jwt-service/models/jwt-model";

export class LoginInputModel {
    email?: string;
    password?: string;
}

export class LoginOutputModel {
    jwtModel?: JwtModel;
}

export class RegisterInputModel {
    firstName!: string;
    middleName!: string;
    lastName!: string;
    email!: string;
    phone!: string;
    dateOfBirth!: Date;
    password!: string;
    confirmedPassword!: string;
}

export class RegisterOutputModel {
    jwtModel?: JwtModel;
}