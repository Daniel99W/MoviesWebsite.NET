import { GetCategoryDto } from "./GetCategoryDto";



export class GetMovieDto 
{
    public id!:string;
    public title!:string;
    public description!:string;
    public addedDate!:Date;
    public vidGuardId!:string;
    public views!:number;
    public upvotes!:number;
    public likes!:number;
    public downvotes!:number;
    public posterImageUrl!:string;
    public categories!:Array<GetCategoryDto>;
}