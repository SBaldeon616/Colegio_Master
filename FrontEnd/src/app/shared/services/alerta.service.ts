import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon, SweetAlertResult } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertaService {
  MostrarSuccess(title: string, message: string, tiempo: number = 5000): Promise<SweetAlertResult<unknown>> {
    return this.MostrarAlerta('success', title, message, tiempo);
  }

  MostrarWarning(title: string, message: string, tiempo: number = 5000): Promise<SweetAlertResult<unknown>> {
    return this.MostrarAlerta('warning', title, message, tiempo);
  }

  MostrarError(title: string, message: string, tiempo: number = 5000): Promise<SweetAlertResult<unknown>> {
    return this.MostrarAlerta('error', title, message, tiempo);
  }

  MostrarErrorHttp(codigoHttp: number, messageBackend?: string, tiempo: number = 5000): Promise<SweetAlertResult<unknown>> {
    const configuracion = this.ObtenerConfiguracionErrorHttp(codigoHttp);
    const messageFinal = messageBackend?.trim() || configuracion.message;

    return this.MostrarAlerta(configuracion.icono, configuracion.title, messageFinal, tiempo);
  }

  MostrarConfirmacionAsync(
    title: string,
    message: string,
    textoConfirmar: string = 'Aceptar',
    textoCancelar: string = 'Cancelar'
  ): Promise<boolean> {
    return Swal.fire({
      icon: 'question',
      title,
      text: message,
      showCancelButton: true,
      confirmButtonText: textoConfirmar,
      cancelButtonText: textoCancelar,
      reverseButtons: true
    }).then((resultado) => resultado.isConfirmed);
  }

  MostrarInformacionHtml(title: string, html: string, tiempo: number = 5000): Promise<SweetAlertResult<unknown>> {
    return Swal.fire({
      icon: 'info',
      title,
      html,
      timer: tiempo,
      timerProgressBar: true,
      confirmButtonText: 'Aceptar'
    });
  }

  MostrarCarga(title: string = 'Cargando...', message: string = 'Por favor espera'): void {
    void Swal.fire({
      title,
      text: message,
      allowOutsideClick: false,
      allowEscapeKey: false,
      didOpen: () => {
        Swal.showLoading();
      }
    });
  }

  MostrarProgreso(title: string, message: string, tiempo: number = 5000): Promise<SweetAlertResult<unknown>> {
    return Swal.fire({
      icon: 'info',
      title,
      text: message,
      timer: tiempo,
      timerProgressBar: true,
      confirmButtonText: 'Aceptar'
    });
  }

  CerrarCarga(): void {
    Swal.close();
  }

  private MostrarAlerta(
    icono: SweetAlertIcon,
    title: string,
    message: string,
    tiempo: number = 5000
  ): Promise<SweetAlertResult<unknown>> {
    return Swal.fire({
      icon: icono,
      title,
      text: message,
      timer: tiempo,
      timerProgressBar: true,
      confirmButtonText: 'Aceptar'
    });
  }

  private ObtenerConfiguracionErrorHttp(codigoHttp: number): {
    icono: SweetAlertIcon;
    title: string;
    message: string;
  } {
    switch (codigoHttp) {
      case 0:
        return {
          icono: 'error',
          title: 'Sin conexion',
          message: 'No se pudo conectar con el servidor. Verifica tu red e intenta nuevamente.'
        };
      case 400:
        return {
          icono: 'warning',
          title: 'Solicitud invalida',
          message: 'La solicitud enviada no es valida. Revisa los datos e intenta nuevamente.'
        };
      case 401:
        return {
          icono: 'error',
          title: 'No autorizado',
          message: 'Tu sesion no es valida o ha expirado. Inicia sesion nuevamente.'
        };
      case 403:
        return {
          icono: 'error',
          title: 'Acceso denegado',
          message: 'No tienes permisos para realizar esta accion.'
        };
      case 404:
        return {
          icono: 'warning',
          title: 'No encontrado',
          message: 'El recurso solicitado no existe o no esta disponible.'
        };
      case 408:
        return {
          icono: 'warning',
          title: 'Tiempo agotado',
          message: 'La solicitud tardo demasiado. Intenta nuevamente.'
        };
      case 409:
        return {
          icono: 'warning',
          title: 'Conflicto de datos',
          message: 'Existe un conflicto con el estado actual de la informacion.'
        };
      case 422:
        return {
          icono: 'warning',
          title: 'Datos no procesables',
          message: 'Los datos enviados no cumplen con las reglas de validacion.'
        };
      case 429:
        return {
          icono: 'warning',
          title: 'Demasiadas solicitudes',
          message: 'Haz superado el limite de intentos. Espera un momento e intenta de nuevo.'
        };
      case 500:
        return {
          icono: 'error',
          title: 'Error interno del servidor',
          message: 'Ocurrio un error inesperado en el servidor.'
        };
      case 502:
      case 503:
      case 504:
        return {
          icono: 'error',
          title: 'Servidor no disponible',
          message: 'El servidor no esta disponible temporalmente. Intenta nuevamente en unos minutos.'
        };
      default:
        return {
          icono: 'error',
          title: `Error HTTP ${codigoHttp}`,
          message: 'Ocurrio un error durante la comunicacion con el servidor.'
        };
    }
  }
}
