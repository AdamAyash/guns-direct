
export class BaseServerResponse<ResultData>{
    dateTimeStamp?: Date;
    isSuccessful?: boolean;
    isCacheData? : boolean;
    result?: ResultData;
}