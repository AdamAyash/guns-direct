
export class BaseServerResponse<ResultData>{
    date?: Date;
    isSuccessful?: boolean;
    result?: ResultData;
}