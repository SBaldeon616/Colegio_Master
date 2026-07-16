import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiEndpoints } from '../../shared';

export type JuegoRecreativoDto = {
  id: number;
  nombre: string;
  tipo?: string;
  estado?: string;
  fechaCreacion?: string;
  fechaModificacion?: string;
  usuarioCreacion?: string;
  usuarioModificacion?: string;
};

export type JuegoRecreativoCrearDto = {
  nombre: string;
  tipo?: string;
  estado?: string;
  usuarioCreacion: string;
};

export type JuegoRecreativoActualizarDto = {
  nombre: string;
  tipo?: string;
  estado?: string;
  usuarioModificacion?: string;
};

@Injectable({
  providedIn: 'root'
})
export class JuegoRecreativoService {
  private readonly _http = inject(HttpClient);
  private readonly apiUrl = `${apiEndpoints.juegoRecreativo}`;

  constructor() { }

  getAll(): Observable<JuegoRecreativoDto[]> {
    return this._http.get<JuegoRecreativoDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<JuegoRecreativoDto> {
    return this._http.get<JuegoRecreativoDto>(`${this.apiUrl}/${id}`);
  }

  create(payload: JuegoRecreativoCrearDto): Observable<JuegoRecreativoDto> {
    return this._http.post<JuegoRecreativoDto>(this.apiUrl, payload);
  }

  update(id: number, payload: JuegoRecreativoActualizarDto): Observable<JuegoRecreativoDto> {
    return this._http.put<JuegoRecreativoDto>(`${this.apiUrl}/${id}`, payload);
  }

  delete(id: number): Observable<boolean> {
    return this._http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}