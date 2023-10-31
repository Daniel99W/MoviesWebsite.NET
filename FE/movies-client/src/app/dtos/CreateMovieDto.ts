import { TagDto } from "./TagDto";




export class CreateMovieDto 
{
    public Id!:string;
    public Title!:string;
    public Description!:string;
    public AddedDate!:Date;
    public PosterImageUrl!:string;
    public PosterImageUrlGif!:string;
    public VidGuardId!:string;
    public CategoriesIds!:string[]
    public Tags!:TagDto[];
    public FirebaseId!:string;
}