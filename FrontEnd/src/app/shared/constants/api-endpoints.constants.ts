import { environment } from "../../../environments/environment";


const dominio = environment.dominio;

export const apiEndpoints = {
  auth: `${dominio}/api/auth`,
  estadoCliente: `${dominio}/api/estadocliente`,
  juegoRecreativo: `${dominio}/api/juegorecreativo`,
  mascota: `${dominio}/api/mascota`
} as const;