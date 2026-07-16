import { Injectable } from '@angular/core';
import { AlmacenSeguroService } from './almacen-seguro.service';

@Injectable({
  providedIn: 'root'
})
export class TokenSesionService {
  private readonly tokenKey = 'auth_token';
  private tokenCache: string | null = null;
  private tokenCacheInicializado = false;

  constructor(private readonly almacenSeguroService: AlmacenSeguroService) { }

  async GuardarToken(token: string): Promise<void> {
    await this.almacenSeguroService.GuardarEncriptado(this.tokenKey, token);
    this.tokenCache = token;
    this.tokenCacheInicializado = true;
  }

  async ObtenerToken(): Promise<string | null> {
    if (this.tokenCacheInicializado) {
      return this.tokenCache;
    }

    const token = await this.almacenSeguroService.ObtenerDesencriptado(this.tokenKey);
    this.tokenCache = token;
    this.tokenCacheInicializado = true;

    return token;
  }

  async EstaAutenticado(): Promise<boolean> {
    return !!(await this.ObtenerToken());
  }

  async CerrarSesion(): Promise<void> {
    await this.almacenSeguroService.Eliminar(this.tokenKey);
    this.tokenCache = null;
    this.tokenCacheInicializado = true;
  }
}
