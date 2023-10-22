import { Languages } from "../enums/Languages";



export class CreateMovieDto 
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
    public CategoriesIds!:string[]
}