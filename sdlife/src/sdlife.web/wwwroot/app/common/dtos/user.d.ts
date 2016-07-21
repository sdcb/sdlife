declare namespace sdlife {
    interface IUserDto {
        id: number;
        userName: string;
        email: string;
    }

    interface IAccountingAuthorizedUser {
        userId: number;
        userName: string;
    }
}
