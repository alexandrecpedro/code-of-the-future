import { User } from "../shared/interfaces/user.interface";

export interface LoginResponse {
    user: User;
    token: string;
}