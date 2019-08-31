export class User{
    Id: string;
    UserName: string;
    Password: string;
    ConfirmPassword: string;
    Email: string;
    Name: string;
    Surname: string;
    Address: string;
    Date: string;
    PhoneNumber: string;
    TypeId: string;
    ImageUrl: string;
    Verificate: number;
}

export class raspored{
    polasci: string
}

export class klasaPodaci{
    id:number;
    dan:string;
}
export class linja{
    linije:number[];
}
export class LinijaZaHub{
    imeLinije:string;
    constructor(i:string){
        this.imeLinije=i;
    }
}