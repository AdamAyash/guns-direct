export interface IServiceResultProcessable<OutputModelType> { 
    processResult: (prcessResult: OutputModelType) => boolean;
    processError: () => void;
 }
