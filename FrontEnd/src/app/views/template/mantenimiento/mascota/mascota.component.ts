import { ChangeDetectionStrategy, Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  MascotaActualizarDto,
  MascotaCrearDto,
  MascotaDto,
  MascotaService
} from '../../../../services/Mascota/Mascota.service';
import { AlertaService } from '../../../../shared/services';

type MascotaVista = {
  id: number;
  categoriaMascota: string;
  raza: string;
  edad: number | null;
  nombre: string;
};

@Component({
  selector: 'app-mascota',
  templateUrl: './mascota.component.html',
  styleUrls: ['./mascota.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, ReactiveFormsModule]
})
export class MascotaComponent implements OnInit {
  private readonly mascotaService = inject(MascotaService);
  private readonly alertaService = inject(AlertaService);
  private readonly fb = inject(FormBuilder);

  protected readonly cargando = signal(false);
  protected readonly guardando = signal(false);
  protected readonly modoEdicion = signal(false);
  protected readonly mascotaEdicionId = signal<number | null>(null);
  protected readonly mascotas = signal<MascotaVista[]>([]);

  protected readonly formulario = this.fb.group({
    nombre: ['', [Validators.required, Validators.maxLength(50)]],
    categoriaMascota: ['', [Validators.maxLength(50)]],
    raza: ['', [Validators.maxLength(50)]],
    edad: [null as number | null, [Validators.min(0), Validators.max(100)]]
  });

  constructor() { }

  ngOnInit() {
    this.ListarMascota();
  }

  protected ListarMascota(): void {
    this.cargando.set(true);

    this.mascotaService.getAll().subscribe({
      next: (response) => {
        this.mascotas.set(this.NormalizarListado(response));
        this.cargando.set(false);
      },
      error: () => {
        this.cargando.set(false);
      }
    });
  }

  protected GuardarMascota(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.guardando.set(true);

    if (this.modoEdicion() && this.mascotaEdicionId() !== null) {
      this.ActualizarMascota(this.mascotaEdicionId() as number, this.ObtenerPayloadActualizar());
      return;
    }

    this.CrearMascota(this.ObtenerPayloadCrear());
  }

  protected EditarMascota(item: MascotaVista): void {
    this.modoEdicion.set(true);
    this.mascotaEdicionId.set(item.id);

    this.formulario.patchValue({
      nombre: item.nombre,
      categoriaMascota: item.categoriaMascota,
      raza: item.raza,
      edad: item.edad
    });
  }

  protected async EliminarMascota(id: number): Promise<void> {
    const confirmado = await this.alertaService.MostrarConfirmacionAsync(
      'Eliminar mascota',
      'Esta accion no se puede deshacer. Deseas continuar?'
    );

    if (!confirmado) {
      return;
    }

    this.mascotaService.delete(id).subscribe({
      next: () => {
        this.ListarMascota();
      },
      error: () => {
      }
    });
  }

  protected CancelarEdicion(): void {
    this.modoEdicion.set(false);
    this.mascotaEdicionId.set(null);
    this.formulario.reset({
      nombre: '',
      categoriaMascota: '',
      raza: '',
      edad: null
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

  protected ObtenerErrorCategoria(): string {
    const control = this.formulario.get('categoriaMascota');

    if (!control || !control.touched) {
      return '';
    }

    if (control.hasError('maxlength')) {
      return 'La categoria excede el maximo permitido.';
    }

    return '';
  }

  protected ObtenerErrorRaza(): string {
    const control = this.formulario.get('raza');

    if (!control || !control.touched) {
      return '';
    }

    if (control.hasError('maxlength')) {
      return 'La raza excede el maximo permitido.';
    }

    return '';
  }

  protected ObtenerErrorEdad(): string {
    const control = this.formulario.get('edad');

    if (!control || !control.touched) {
      return '';
    }

    if (control.hasError('min')) {
      return 'La edad no puede ser negativa.';
    }

    if (control.hasError('max')) {
      return 'La edad excede el maximo permitido.';
    }

    return '';
  }

  private CrearMascota(payload: MascotaCrearDto): void {
    this.mascotaService.create(payload).subscribe({
      next: () => {
        this.guardando.set(false);
        this.CancelarEdicion();
        this.ListarMascota();
      },
      error: () => {
        this.guardando.set(false);
      }
    });
  }

  private ActualizarMascota(id: number, payload: MascotaActualizarDto): void {
    this.mascotaService.update(id, payload).subscribe({
      next: () => {
        this.guardando.set(false);
        this.CancelarEdicion();
        this.ListarMascota();
      },
      error: () => {
        this.guardando.set(false);
      }
    });
  }

  private ObtenerPayloadCrear(): MascotaCrearDto {
    const formValue = this.formulario.getRawValue();

    return {
      id: 0,
      nombre: (formValue.nombre ?? '').trim(),
      categoriaMascota: (formValue.categoriaMascota ?? '').trim(),
      raza: (formValue.raza ?? '').trim(),
      edad: formValue.edad !== null ? Number(formValue.edad) : null,
      usuarioCreacion: 'admin'
    };
  }

  private ObtenerPayloadActualizar(): MascotaActualizarDto {
    const formValue = this.formulario.getRawValue();

    return {
      id: this.mascotaEdicionId() ?? 0,
      nombre: (formValue.nombre ?? '').trim(),
      categoriaMascota: (formValue.categoriaMascota ?? '').trim(),
      raza: (formValue.raza ?? '').trim(),
      edad: formValue.edad !== null ? Number(formValue.edad) : null,
      usuarioCreacion: 'admin',
      usuarioModificacion: 'admin'
    };
  }

  private NormalizarListado(response: MascotaDto[] | null | undefined): MascotaVista[] {
    if (!Array.isArray(response)) {
      return [];
    }

    return response.map((item, index) => {
      const data = item as unknown as Record<string, unknown>;

      const id = this.ValorNumerico(data, ['id', 'Id'], index + 1);
      const categoriaMascota = this.ValorTexto(data, ['categoriaMascota', 'CategoriaMascota'], '');
      const raza = this.ValorTexto(data, ['raza', 'Raza'], '');
      const nombre = this.ValorTexto(data, ['nombre', 'Nombre'], '');
      const edad = this.ValorNumericoNulable(data, ['edad', 'Edad']);

      return {
        id,
        categoriaMascota,
        raza,
        edad,
        nombre
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

  private ValorNumericoNulable(data: Record<string, unknown>, keys: string[]): number | null {
    for (const key of keys) {
      const value = data[key];

      if (typeof value === 'number' && Number.isFinite(value)) {
        return value;
      }
    }

    return null;
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