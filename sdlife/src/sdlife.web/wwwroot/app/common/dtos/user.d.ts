declare namespace sdlife {
    interface IUserDto {
        id: number;
        userName: string;
        email: string;
    }

    interface IAccountingUserRelationship {
        userId: number;
        userName: string;
    }
}
