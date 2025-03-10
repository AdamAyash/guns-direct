import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  get(key: string) {
      return JSON.parse(localStorage.getItem(key) || '{}') || {};
  }
  set(key: string, value: any): boolean {
    localStorage.setItem(key, JSON.stringify(value));
    return true;
  }
  has(key: string): boolean {
    return !!localStorage.getItem(key);
  }
  remove(key: string) {
    localStorage.removeItem(key);
  }
  clear() {
    localStorage.clear();
  }

  public isInitialized(): boolean | Storage{
      return (typeof window !== 'undefined' && window.localStorage);
  }
}