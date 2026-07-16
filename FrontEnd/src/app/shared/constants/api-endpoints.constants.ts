import { environment } from "../../../environments/environment";


const dominio = environment.dominio;

export const apiEndpoints = {
  auth: `${dominio}/api/auth`,
  estadoCliente: `${dominio}/api/estadocliente`
} as const;
