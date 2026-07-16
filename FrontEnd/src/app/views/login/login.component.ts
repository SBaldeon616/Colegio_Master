import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from './services/auth.service';
import { AuthResponse } from './models/auth-response.mode';
import { AuthRequest } from './models/auth-request.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    ReactiveFormsModule,
    RouterModule
  ]
})
export class LoginComponent {
  private readonly _fb = inject(FormBuilder);
  private readonly _router = inject(Router);
  private readonly _authService = inject(AuthService);

  protected readonly formularioIngreso = this._fb.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]]
  });

  protected SePuedeIngresar(): boolean {
    return this.formularioIngreso.valid ?? false;
  }

  protected ObtenerErrores(): string[] {
    const errores: string[] = [];
    const controlUsuario = this.formularioIngreso.get('username');
    const controlContrasena = this.formularioIngreso.get('password');

    if (controlUsuario?.hasError('required') && controlUsuario?.touched) {
      errores.push('El campo usuario es requerido.');
    }

    if (controlContrasena?.hasError('required') && controlContrasena?.touched) {
      errores.push('El campo password es requerido.');
    }

    return errores;
  }

  protected ProcesarIngreso(): void {
    console.log('Token obtenido:', "qqqqq");
    if (!this.formularioIngreso.valid) {
      this.formularioIngreso.markAllAsTouched();
      return;
    }
    console.log('Token obtenido:', "fffff");
    const lgRequest: AuthRequest = this.formularioIngreso.getRawValue() as AuthRequest;
    console.log('Token obtenido:', "00000");
    this._authService.ingresar(lgRequest).subscribe({
      next: async (response: AuthResponse) => {
        console.log('Token obtenido:', "111111");
        const token = this.ObtenerTokenRespuesta(response);
        console.log('Token obtenido:', token);
        if (token) {
          console.log('Token obtenido:', "44444444444");
          await this._authService.GuardarToken(token);
        }

        void this._router.navigate(['/dashboard']);
      },
      error: (error: unknown) => {
        console.error('Error al procesar el ingreso:', error);
      }
    });
  }

  private ObtenerTokenRespuesta(response: unknown): string | null {
    if (!response || typeof response !== 'object') {
      return null;
    }

    const responseBody = response as Record<string, unknown>;

    if (typeof responseBody['token'] === 'string' && responseBody['token'].trim() !== '') {
      return responseBody['token'];
    }

    if (typeof responseBody['Token'] === 'string' && responseBody['Token'].trim() !== '') {
      return responseBody['Token'];
    }

    const content = responseBody['content'] ?? responseBody['Content'];

    if (!content || typeof content !== 'object') {
      return null;
    }

    const contentBody = content as Record<string, unknown>;

    if (typeof contentBody['token'] === 'string' && contentBody['token'].trim() !== '') {
      return contentBody['token'];
    }

    if (typeof contentBody['Token'] === 'string' && contentBody['Token'].trim() !== '') {
      return contentBody['Token'];
    }

    return null;
  }
}
