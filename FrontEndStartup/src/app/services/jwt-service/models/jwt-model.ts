import { Utitlities } from "../../../utilities/utilities";

export interface JwtModel {
  [prop: string]: any;
  payload: string;
  userId?: string;
  tokenType?: string;
  expiresIn?: number;
  exp?: number;
}

export class JwtWrapper {
  constructor(protected attributes: JwtModel) {}
  get payload(): string {
    return this.attributes.payload;
  }
  get userId(): string {
    return this.attributes.userId ?? '';
  }
  get tokenType(): string {
    return this.attributes.tokenType ?? 'bearer';
  }
  get exp(): number | void {
    return this.attributes.exp;
  }
  valid(): boolean {
    return this.hasAccessToken() && !this.isExpired();
  }
  getBearerToken(): string {
    return this.payload
      ? [Utitlities.capitalize(this.tokenType), this.payload].join(' ').trim()
      : '';
  }
  private hasAccessToken(): boolean {
    return !!this.payload;
  }

  private isExpired(): boolean {
    return this.exp !== undefined && this.exp - Utitlities.currentTimestamp() <= 0;
  }
}