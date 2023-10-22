


export class GetMoviesQueryParametersDto
{
    ItemsPerPage!:number;
    Page!:number;
    Title:string;
    CategoriesIds!:string[];
    BeginAddedDate:Date;
    EndAddedDate:Date;

    constructor()
    {
        this.ItemsPerPage = 15;
        this.Page = 1;
        this.CategoriesIds = [];
        this.Title = '';
        this.BeginAddedDate = new Date();
        this.EndAddedDate = new Date();
    }
}