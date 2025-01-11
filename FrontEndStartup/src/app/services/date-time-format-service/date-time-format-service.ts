import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateTimeFormatService {

  constructor() {

   }

   public getDateNewDateFromString(dateTimeString: string): Date{
      return new Date(dateTimeString);
   }
}
