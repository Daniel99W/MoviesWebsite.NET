

export class PaginatedResultDto<T>
{
    CurrentPage!:number;
    TotalPages!:number;
    Results!:T[];
}
