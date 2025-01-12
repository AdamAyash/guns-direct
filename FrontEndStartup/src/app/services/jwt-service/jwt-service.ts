import { Injectable } from "@angular/core";
import { Utitlities } from "../../utilities/utilities";
import { JwtModel, JwtWrapper } from "./models/jwt-model";
import { LocalStorageService } from "../local-storage-service/local-storage-service";

@Injectable({
  providedIn: 'root',
})
export class JwtService {

  private readonly key = 'jwt';
  private _token?: JwtWrapper;

  constructor(private localStorageService: LocalStorageService) {}

  private get token(): JwtWrapper | undefined {
    if (!this._token && this.localStorageService.isInitialized()) {
      this._token = new JwtWrapper(this.localStorageService.get(this.key));
    }
    return this._token;
  }
  set(token?: JwtModel): JwtService {
    this.save(token);
    return this;
  }
  clear(): void {
    this.save();
  }
  isValid(): boolean {
  return this.token?.valid() ?? false;
  }
  getUserid(): string {
    return this.token?.userId ?? '';
  }
  getBearerToken(): string {
    return this.token?.getBearerToken() ?? '';
  }
  ngOnDestroy(): void {
  }
  
  private save(token?: JwtModel): void {
    this._token = undefined;
    if (!token) {
      this.localStorageService.remove(this.key);
    } else {
      const value = Object.assign({ access_token: '', token_type: 'Bearer' }, token, {
        exp: token.expiresIn ? Utitlities.currentTimestamp() + token.expiresIn : null,
      });
      this.localStorageService.set(this.key, Utitlities.filterObject(value));
    }
  }

}