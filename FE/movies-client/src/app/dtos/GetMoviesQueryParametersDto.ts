


export class GetMoviesQueryParametersDto
{
    ItemsPerPage!:number;
    Page!:number;
    Title:string;
    CategoriesIds!:string[];
    BeginAddedDate:Date|undefined;
    EndAddedDate:Date|undefined;

    constructor()
    {
        this.ItemsPerPage = 30;
        this.Page = 1;
        this.CategoriesIds = [];
        this.Title = '';
        this.BeginAddedDate = undefined;
        this.EndAddedDate = undefined;
    }
}