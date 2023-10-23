



export class CreateMovieDto 
{
    public Id!:string;
    public Title!:string;
    public Description!:string;
    public AddedDate!:Date;
    public PosterImageUrl!:string;
    public VidGuardId!:string;
    public CategoriesIds!:string[]
}