

export class PaginatedResultDto<T>
{
    Page!:number;
    TotalPages!:number;
    Results!:T[];
}
