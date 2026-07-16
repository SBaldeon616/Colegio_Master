import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthResponse } from '../models/auth-response.mode';
import { AuthRequest } from '../models/auth-request.model';
import { Observable } from 'rxjs/internal/Observable';
import { apiEndpoints, TokenSesionService } from '../../../shared';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly _http = inject(HttpClient);
  private readonly _tokenSesionService = inject(TokenSesionService);
  private readonly apiUrl = apiEndpoints.auth;

  constructor() { }

  ingresar(request: AuthRequest): Observable<AuthResponse> {
    return this._http.post<AuthResponse>(this.apiUrl, request);
  }

  async EstaAutenticado(): Promise<boolean> {
    return this._tokenSesionService.EstaAutenticado();
  }

  async GuardarToken(token: string): Promise<void> {
    await this._tokenSesionService.GuardarToken(token);
  }

  async ObtenerToken(): Promise<string | null> {
    return this._tokenSesionService.ObtenerToken();
  }

  async CerrarSesion(): Promise<void> {
    await this._tokenSesionService.CerrarSesion();
  }
}
