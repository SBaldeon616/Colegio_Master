import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiEndpoints } from '../../shared';

export type MascotaDto = {
  id: number;
  categoriaMascota?: string;
  raza?: string;
  edad?: number | null;
  nombre?: string;
  fechaCreacion?: string;
  fechaModificacion?: string;
  usuarioCreacion?: string;
  usuarioModificacion?: string;
};

export type MascotaCrearDto = {
  id: number;
  categoriaMascota?: string;
  raza?: string;
  edad?: number | null;
  nombre?: string;
  usuarioCreacion: string;
  usuarioModificacion?: string;
};

export type MascotaActualizarDto = {
  id: number;
  categoriaMascota?: string;
  raza?: string;
  edad?: number | null;
  nombre?: string;
  usuarioCreacion: string;
  usuarioModificacion?: string;
};

@Injectable({
  providedIn: 'root'
})
export class MascotaService {
  private readonly _http = inject(HttpClient);
  private readonly apiUrl = `${apiEndpoints.mascota}`;

  constructor() { }

  getAll(): Observable<MascotaDto[]> {
    return this._http.get<MascotaDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<MascotaDto> {
    return this._http.get<MascotaDto>(`${this.apiUrl}/${id}`);
  }

  create(payload: MascotaCrearDto): Observable<MascotaDto> {
    return this._http.post<MascotaDto>(this.apiUrl, payload);
  }

  update(id: number, payload: MascotaActualizarDto): Observable<MascotaDto> {
    return this._http.put<MascotaDto>(`${this.apiUrl}/${id}`, payload);
  }

  delete(id: number): Observable<boolean> {
    return this._http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}