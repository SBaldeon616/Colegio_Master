import { ChangeDetectionStrategy, Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  JuegoRecreativoActualizarDto,
  JuegoRecreativoCrearDto,
  JuegoRecreativoDto,
  JuegoRecreativoService
} from '../../../../services/JuegoRecreativo/JuegoRecreativo.service';
import { AlertaService } from '../../../../shared/services';
 
type JuegoRecreativoVista = {
  id: number;
  nombre: string;
  tipo: string;
  estado: string;
};
 
@Component({
  selector: 'app-juego-recreativo',
  templateUrl: './juego-recreativo.component.html',
  styleUrls: ['./juego-recreativo.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, ReactiveFormsModule]
})
export class JuegoRecreativoComponent implements OnInit {
  private readonly juegoRecreativoService = inject(JuegoRecreativoService);
  private readonly alertaService = inject(AlertaService);
  private readonly fb = inject(FormBuilder);
 
  protected readonly cargando = signal(false);
  protected readonly guardando = signal(false);
  protected readonly modoEdicion = signal(false);
  protected readonly juegoEdicionId = signal<number | null>(null);
  protected readonly juegos = signal<JuegoRecreativoVista[]>([]);
 
  protected readonly formulario = this.fb.group({
    nombre: ['', [Validators.required, Validators.maxLength(100)]],
    tipo: ['', [Validators.maxLength(50)]],
    estado: ['', [Validators.maxLength(50)]]
  });
 
  constructor() { }
 
  ngOnInit() {
    this.ListarJuegoRecreativo();
  }
 
  protected ListarJuegoRecreativo(): void {
    this.cargando.set(true);
 
    this.juegoRecreativoService.getAll().subscribe({
      next: (response) => {
        this.juegos.set(this.NormalizarListado(response));
        this.cargando.set(false);
      },
      error: () => {
        this.cargando.set(false);
      }
    });
  }
 
  protected GuardarJuegoRecreativo(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }
 
    this.guardando.set(true);
 
    if (this.modoEdicion() && this.juegoEdicionId() !== null) {
      this.ActualizarJuegoRecreativo(this.juegoEdicionId() as number, this.ObtenerPayloadActualizar());
      return;
    }
 
    this.CrearJuegoRecreativo(this.ObtenerPayloadCrear());
  }
 
  protected EditarJuegoRecreativo(item: JuegoRecreativoVista): void {
    this.modoEdicion.set(true);
    this.juegoEdicionId.set(item.id);
 
    this.formulario.patchValue({
      nombre: item.nombre,
      tipo: item.tipo,
      estado: item.estado
    });
  }
 
  protected async EliminarJuegoRecreativo(id: number): Promise<void> {
    const confirmado = await this.alertaService.MostrarConfirmacionAsync(
      'Eliminar juego recreativo',
      'Esta accion no se puede deshacer. Deseas continuar?'
    );
 
    if (!confirmado) {
      return;
    }
 
    this.juegoRecreativoService.delete(id).subscribe({
      next: () => {
        this.ListarJuegoRecreativo();
      },
      error: () => {
      }
    });
  }
 
  protected CancelarEdicion(): void {
    this.modoEdicion.set(false);
    this.juegoEdicionId.set(null);
    this.formulario.reset({
      nombre: '',
      tipo: '',
      estado: ''
    });
  }
 
  protected ObtenerErrorNombre(): string {
    const control = this.formulario.get('nombre');
 
    if (!control || !control.touched) {
      return '';
    }
 
    if (control.hasError('required')) {
      return 'El nombre es obligatorio.';
    }
 
    if (control.hasError('maxlength')) {
      return 'El nombre excede el maximo permitido.';
    }
 
    return '';
  }
 
  protected ObtenerErrorTipo(): string {
    const control = this.formulario.get('tipo');
 
    if (!control || !control.touched) {
      return '';
    }
 
    if (control.hasError('maxlength')) {
      return 'El tipo excede el maximo permitido.';
    }
 
    return '';
  }
 
  protected ObtenerErrorEstado(): string {
    const control = this.formulario.get('estado');
 
    if (!control || !control.touched) {
      return '';
    }
 
    if (control.hasError('maxlength')) {
      return 'El estado excede el maximo permitido.';
    }
 
    return '';
  }
 
  private CrearJuegoRecreativo(payload: JuegoRecreativoCrearDto): void {
    this.juegoRecreativoService.create(payload).subscribe({
      next: () => {
        this.guardando.set(false);
        this.CancelarEdicion();
        this.ListarJuegoRecreativo();
      },
      error: () => {
        this.guardando.set(false);
      }
    });
  }
 
  private ActualizarJuegoRecreativo(id: number, payload: JuegoRecreativoActualizarDto): void {
    this.juegoRecreativoService.update(id, payload).subscribe({
      next: () => {
        this.guardando.set(false);
        this.CancelarEdicion();
        this.ListarJuegoRecreativo();
      },
      error: () => {
        this.guardando.set(false);
      }
    });
  }
 
  private ObtenerPayloadCrear(): JuegoRecreativoCrearDto {
    const formValue = this.formulario.getRawValue();
 
    return {
      nombre: (formValue.nombre ?? '').trim(),
      tipo: (formValue.tipo ?? '').trim(),
      estado: (formValue.estado ?? '').trim(),
      usuarioCreacion: 'admin'
    };
  }
 
  private ObtenerPayloadActualizar(): JuegoRecreativoActualizarDto {
    const formValue = this.formulario.getRawValue();
 
    return {
      nombre: (formValue.nombre ?? '').trim(),
      tipo: (formValue.tipo ?? '').trim(),
      estado: (formValue.estado ?? '').trim(),
      usuarioModificacion: 'admin'
    };
  }
 
  private NormalizarListado(response: JuegoRecreativoDto[] | null | undefined): JuegoRecreativoVista[] {
    if (!Array.isArray(response)) {
      return [];
    }
 
    return response.map((item, index) => {
      const data = item as unknown as Record<string, unknown>;
 
      const id = this.ValorNumerico(data, ['id', 'Id'], index + 1);
      const nombre = this.ValorTexto(data, ['nombre', 'Nombre'], '');
      const tipo = this.ValorTexto(data, ['tipo', 'Tipo'], '');
      const estado = this.ValorTexto(data, ['estado', 'Estado'], '');
 
      return {
        id,
        nombre,
        tipo,
        estado
      };
    });
  }
 
  private ValorNumerico(data: Record<string, unknown>, keys: string[], fallback: number): number {
    for (const key of keys) {
      const value = data[key];
 
      if (typeof value === 'number' && Number.isFinite(value)) {
        return value;
      }
    }
 
    return fallback;
  }
 
  private ValorTexto(data: Record<string, unknown>, keys: string[], fallback: string): string {
    for (const key of keys) {
      const value = data[key];
 
      if (typeof value === 'string') {
        return value;
      }
    }
 
    return fallback;
  }
 
}
 






