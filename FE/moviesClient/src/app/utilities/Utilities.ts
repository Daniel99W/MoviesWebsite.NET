import { formatDate } from "@angular/common";


export class Utilities 
{
    public static formatDate(date:Date)
    {
        return formatDate(date,'yyyy-MM-dd','en_US');
    }

    public static wordMaxSize(word:string,size:number)
    {
        return word.substring(0,size)+'...';
    }
}