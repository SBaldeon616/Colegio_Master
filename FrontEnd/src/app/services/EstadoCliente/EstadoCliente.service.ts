import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiEndpoints } from '../../shared';

export type EstadoClienteDto = {
  id: number;
  codigo?: string;
  descripcion?: string;
};

export type EstadoClienteCrearDto = {
  id: number;
  codigo: string;
  descripcion: string;
};

export type EstadoClienteActualizarDto = {
  id: number;
  codigo?: string;
  descripcion?: string;
};

@Injectable({
  providedIn: 'root'
})
export class EstadoClienteService {
  private readonly _http = inject(HttpClient);
  private readonly apiUrl = `${apiEndpoints.estadoCliente}`;

  constructor() { }

  getAll(): Observable<EstadoClienteDto[]> {
    return this._http.get<EstadoClienteDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<EstadoClienteDto> {
    return this._http.get<EstadoClienteDto>(`${this.apiUrl}/${id}`);
  }

  create(payload: EstadoClienteCrearDto): Observable<EstadoClienteDto> {
    return this._http.post<EstadoClienteDto>(this.apiUrl, payload);
  }

  update(id: number, payload: EstadoClienteActualizarDto): Observable<EstadoClienteDto> {
    return this._http.put<EstadoClienteDto>(`${this.apiUrl}/${id}`, payload);
  }

  delete(id: number): Observable<boolean> {
    return this._http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}


