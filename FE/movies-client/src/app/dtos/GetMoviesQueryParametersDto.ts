


export class GetMoviesQueryParametersDto
{
    ItemsPerPage!:number;
    Page!:number;
    Title:string;
    CategoriesIds!:string[];
    BeginReleaseDate:number;
    EndReleaseDate:number;

    constructor()
    {
        this.ItemsPerPage = 15;
        this.Page = 1;
        this.CategoriesIds = [];
        this.Title = '';
        this.BeginReleaseDate = 1990;
        this.EndReleaseDate = new Date().getFullYear();
    }
}