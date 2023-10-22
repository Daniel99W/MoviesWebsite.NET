import { Languages } from "../enums/Languages";
import { GetCategoryDto } from "./GetCategoryDto";



export class GetMovieDto 
{
    public Id!:string;
    public Title!:string;
    public TrailerLink!:string;
    public Description!:string;
    public Language!:Languages;
    public Subtitle!:Languages;
    public AddedDate!:Date;
    public PosterImage!:string;
    public MovieReleaseDate!:number;
    public Categories!:GetCategoryDto;
}