import { ChangeDetectionStrategy, Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  EstadoClienteActualizarDto,
  EstadoClienteCrearDto,
  EstadoClienteDto,
  EstadoClienteService
} from '../../../../services/EstadoCliente/EstadoCliente.service';
import { AlertaService } from '../../../../shared/services';

type EstadoClienteVista = {
  id: number;
  codigo: string;
  descripcion: string;
};

@Component({
  selector: 'app-estado-cliente',
  templateUrl: './estado-cliente.component.html',
  styleUrls: ['./estado-cliente.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, ReactiveFormsModule]
})
export class EstadoClienteComponent implements OnInit {
  private readonly estadoClienteService = inject(EstadoClienteService);
  private readonly alertaService = inject(AlertaService);
  private readonly fb = inject(FormBuilder);

  protected readonly cargando = signal(false);
  protected readonly guardando = signal(false);
  protected readonly modoEdicion = signal(false);
  protected readonly estadoClienteEdicionId = signal<number | null>(null);
  protected readonly estadosCliente = signal<EstadoClienteVista[]>([]);

  protected readonly formulario = this.fb.group({
    id: [null as number | null, [Validators.required, Validators.min(1)]],
    codigo: ['', [Validators.required, Validators.maxLength(50)]],
    descripcion: ['', [Validators.required, Validators.maxLength(200)]]
  });

  constructor() { }

  ngOnInit() {
    this.ListarEstadoCliente();
  }

  protected ListarEstadoCliente(): void {
    this.cargando.set(true);

    this.estadoClienteService.getAll().subscribe({
      next: (response) => {
        this.estadosCliente.set(this.NormalizarListado(response));
        this.cargando.set(false);
      },
      error: () => {
        this.cargando.set(false);
      }
    });
  }

  protected GuardarEstadoCliente(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    const payload = this.ObtenerPayloadFormulario();
    this.guardando.set(true);

    if (this.modoEdicion() && this.estadoClienteEdicionId() !== null) {
      this.ActualizarEstadoCliente(this.estadoClienteEdicionId() as number, payload);
      return;
    }

    this.CrearEstadoCliente(payload);
  }

  protected EditarEstadoCliente(item: EstadoClienteVista): void {
    this.modoEdicion.set(true);
    this.estadoClienteEdicionId.set(item.id);

    this.formulario.patchValue({
      id: item.id,
      codigo: item.codigo,
      descripcion: item.descripcion
    });

    this.formulario.controls.id.disable({ emitEvent: false });
  }

  protected async EliminarEstadoCliente(id: number): Promise<void> {
    const confirmado = await this.alertaService.MostrarConfirmacionAsync(
      'Eliminar estado de cliente',
      'Esta accion no se puede deshacer. Deseas continuar?'
    );

    if (!confirmado) {
      return;
    }

    this.estadoClienteService.delete(id).subscribe({
      next: () => {
        this.ListarEstadoCliente();
      },
      error: () => {
      }
    });
  }

  protected CancelarEdicion(): void {
    this.modoEdicion.set(false);
    this.estadoClienteEdicionId.set(null);
    this.formulario.controls.id.enable({ emitEvent: false });
    this.formulario.reset({
      id: null,
      codigo: '',
      descripcion: ''
    });
  }

  protected ObtenerErrorId(): string {
    const control = this.formulario.get('id');

    if (!control || !control.touched) {
      return '';
    }

    if (control.hasError('required')) {
      return 'El Id es obligatorio.';
    }

    if (control.hasError('min')) {
      return 'El Id debe ser mayor a cero.';
    }

    return '';
  }

  protected ObtenerErrorCodigo(): string {
    const control = this.formulario.get('codigo');

    if (!control || !control.touched) {
      return '';
    }

    if (control.hasError('required')) {
      return 'El codigo es obligatorio.';
    }

    if (control.hasError('maxlength')) {
      return 'El codigo excede el maximo permitido.';
    }

    return '';
  }

  protected ObtenerErrorDescripcion(): string {
    const control = this.formulario.get('descripcion');

    if (!control || !control.touched) {
      return '';
    }

    if (control.hasError('required')) {
      return 'La descripcion es obligatoria.';
    }

    if (control.hasError('maxlength')) {
      return 'La descripcion excede el maximo permitido.';
    }

    return '';
  }

  private CrearEstadoCliente(payload: EstadoClienteCrearDto): void {
    this.estadoClienteService.create(payload).subscribe({
      next: () => {
        this.guardando.set(false);
        this.CancelarEdicion();
        this.ListarEstadoCliente();
      },
      error: () => {
        this.guardando.set(false);
      }
    });
  }

  private ActualizarEstadoCliente(id: number, payload: EstadoClienteActualizarDto): void {
    this.estadoClienteService.update(id, payload).subscribe({
      next: () => {
        this.guardando.set(false);
        this.CancelarEdicion();
        this.ListarEstadoCliente();
      },
      error: () => {
        this.guardando.set(false);
      }
    });
  }

  private ObtenerPayloadFormulario(): EstadoClienteCrearDto {
    const formValue = this.formulario.getRawValue();

    return {
      id: Number(formValue.id ?? 0),
      codigo: (formValue.codigo ?? '').trim(),
      descripcion: (formValue.descripcion ?? '').trim()
    };
  }

  private NormalizarListado(response: EstadoClienteDto[] | null | undefined): EstadoClienteVista[] {
    if (!Array.isArray(response)) {
      return [];
    }

    return response.map((item, index) => {
      const data = item as unknown as Record<string, unknown>;

      const id = this.ValorNumerico(data, ['id', 'Id', 'idEstadoCliente', 'IdEstadoCliente'], index + 1);
      const codigo = this.ValorTexto(data, ['codigo', 'Codigo', 'code', 'Code'], `EC-${id}`);
      const descripcion = this.ValorTexto(data, ['descripcion', 'Descripcion', 'description', 'Description'], '');

      return {
        id,
        codigo,
        descripcion
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
