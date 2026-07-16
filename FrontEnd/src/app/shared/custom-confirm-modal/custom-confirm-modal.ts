import {
  ChangeDetectionStrategy,
  Component,
  input,
  output
} from '@angular/core';

@Component({
  selector: 'app-custom-confirm-modal',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    class: 'custom-confirm-modal-host'
  },
  template: `
    @if (isOpen()) {
      <section class="backdrop" role="presentation">
        <article
          class="dialog"
          role="dialog"
          aria-modal="true"
          aria-labelledby="confirm-modal-title"
          aria-describedby="confirm-modal-message"
        >
          <h2 id="confirm-modal-title">{{ titulo() }}</h2>
          <p id="confirm-modal-message">{{ mensaje() }}</p>

          <div class="acciones">
            <button type="button" (click)="CancelarAccion()">{{ etiquetaCancelar() }}</button>
            <button type="button" class="primario" (click)="ConfirmarAccion()">
              {{ etiquetaConfirmar() }}
            </button>
          </div>
        </article>
      </section>
    }
  `,
  styles: [
    `
      :host {
        position: fixed;
        inset: 0;
        pointer-events: none;
      }

      .backdrop {
        position: fixed;
        inset: 0;
        background: rgb(0 0 0 / 45%);
        display: grid;
        place-items: center;
        padding: 1rem;
        pointer-events: auto;
      }

      .dialog {
        width: min(28rem, 100%);
        border-radius: 0.75rem;
        background: #fff;
        padding: 1.25rem;
        box-shadow: 0 12px 40px rgb(0 0 0 / 22%);
      }

      h2 {
        margin: 0;
        font-size: 1.125rem;
      }

      p {
        margin: 0.75rem 0 1rem;
      }

      .acciones {
        display: flex;
        justify-content: end;
        gap: 0.5rem;
      }

      button {
        border: 1px solid #c5c5c5;
        border-radius: 0.5rem;
        background: #fff;
        padding: 0.5rem 0.8rem;
        cursor: pointer;
      }

      .primario {
        border-color: #1d4ed8;
        background: #1d4ed8;
        color: #fff;
      }
    `
  ]
})
export class CustomConfirmModal {
  readonly titulo = input<string>('Confirmar venta');
  readonly mensaje = input<string>('Deseas continuar con la confirmacion de la venta?');
  readonly etiquetaConfirmar = input<string>('Confirmar');
  readonly etiquetaCancelar = input<string>('Cancelar');
  readonly isOpen = input<boolean>(false);

  readonly confirmed = output<void>();
  readonly cancelled = output<void>();

  ConfirmarAccion(): void {
    this.confirmed.emit();
  }

  CancelarAccion(): void {
    this.cancelled.emit();
  }
}
