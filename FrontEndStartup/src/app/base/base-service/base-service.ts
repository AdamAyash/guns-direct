import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export abstract class BaseService{

    constructor(private httpClient: HttpClient){
        
    }
    
    public abstract getServiceDomain(): string;
}