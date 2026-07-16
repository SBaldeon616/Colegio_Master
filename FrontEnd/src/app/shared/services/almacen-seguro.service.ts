import { Injectable } from '@angular/core';

type PayloadEncriptado = {
  v: number;
  salt: string;
  iv: string;
  data: string;
};

@Injectable({
  providedIn: 'root'
})
export class AlmacenSeguroService {
  private readonly version = 1;
  private readonly iteraciones = 210000;
  private readonly longitudSalt = 16;
  private readonly longitudIv = 12;
  private readonly secretoAplicacion = 'colegio-master-basico-storage-v1';
  private readonly prefijoClave = '__sec_k__';
  private readonly textEncoder = new TextEncoder();
  private readonly textDecoder = new TextDecoder();

  async GuardarEncriptado(clave: string, valor: string): Promise<void> {
    const claveFisica = await this.ObtenerClaveFisica(clave);
    const salt = this.GenerarBytesAleatorios(this.longitudSalt);
    const iv = this.GenerarBytesAleatorios(this.longitudIv);
    const cryptoKey = await this.DerivarClave(this.AsegurarArrayBuffer(salt));
    const valorBytes = this.textEncoder.encode(valor);
    const cifrado = await crypto.subtle.encrypt(
      { name: 'AES-GCM', iv: this.AsegurarArrayBuffer(iv) },
      cryptoKey,
      this.AsegurarArrayBuffer(valorBytes)
    );

    const payload: PayloadEncriptado = {
      v: this.version,
      salt: this.ConvertirBase64(salt),
      iv: this.ConvertirBase64(iv),
      data: this.ConvertirBase64(new Uint8Array(cifrado))
    };

    localStorage.setItem(claveFisica, JSON.stringify(payload));
    if (claveFisica !== clave) {
      localStorage.removeItem(clave);
    }
  }

  async ObtenerDesencriptado(clave: string): Promise<string | null> {
    const claveFisica = await this.ObtenerClaveFisica(clave);
    const valorGuardadoActual = localStorage.getItem(claveFisica);
    const valorGuardadoLegacy = localStorage.getItem(clave);
    const valorGuardado = valorGuardadoActual ?? valorGuardadoLegacy;

    if (!valorGuardado) {
      return null;
    }

    const payload = this.IntentarLeerPayload(valorGuardado);

    if (!payload) {
      // Migra valores legados en texto plano a formato encriptado.
      await this.GuardarEncriptado(clave, valorGuardado);
      return valorGuardado;
    }

    try {
      const salt = this.DesdeBase64(payload.salt);
      const iv = this.DesdeBase64(payload.iv);
      const data = this.DesdeBase64(payload.data);
      const cryptoKey = await this.DerivarClave(this.AsegurarArrayBuffer(salt));
      const desencriptado = await crypto.subtle.decrypt(
        { name: 'AES-GCM', iv: this.AsegurarArrayBuffer(iv) },
        cryptoKey,
        this.AsegurarArrayBuffer(data)
      );

      if (!valorGuardadoActual && valorGuardadoLegacy) {
        localStorage.setItem(claveFisica, valorGuardadoLegacy);
        localStorage.removeItem(clave);
      }

      return this.textDecoder.decode(desencriptado);
    } catch {
      localStorage.removeItem(claveFisica);
      localStorage.removeItem(clave);
      return null;
    }
  }

  async Eliminar(clave: string): Promise<void> {
    const claveFisica = await this.ObtenerClaveFisica(clave);
    localStorage.removeItem(claveFisica);
    localStorage.removeItem(clave);
  }

  private IntentarLeerPayload(valorGuardado: string): PayloadEncriptado | null {
    try {
      const parsed = JSON.parse(valorGuardado) as Partial<PayloadEncriptado>;

      if (
        parsed
        && parsed.v === this.version
        && typeof parsed.salt === 'string'
        && typeof parsed.iv === 'string'
        && typeof parsed.data === 'string'
      ) {
        return {
          v: parsed.v,
          salt: parsed.salt,
          iv: parsed.iv,
          data: parsed.data
        };
      }
    } catch {
      return null;
    }

    return null;
  }

  private async DerivarClave(salt: ArrayBuffer): Promise<CryptoKey> {
    const baseKey = await crypto.subtle.importKey(
      'raw',
      this.textEncoder.encode(this.secretoAplicacion),
      'PBKDF2',
      false,
      ['deriveKey']
    );

    return crypto.subtle.deriveKey(
      {
        name: 'PBKDF2',
        salt,
        iterations: this.iteraciones,
        hash: 'SHA-256'
      },
      baseKey,
      {
        name: 'AES-GCM',
        length: 256
      },
      false,
      ['encrypt', 'decrypt']
    );
  }

  private GenerarBytesAleatorios(longitud: number): Uint8Array {
    const bytes = new Uint8Array(longitud);
    crypto.getRandomValues(bytes);
    return bytes;
  }

  private AsegurarArrayBuffer(bytes: Uint8Array): ArrayBuffer {
    const copia = Uint8Array.from(bytes);
    return copia.buffer;
  }

  private ConvertirBase64(valor: Uint8Array): string {
    let binary = '';

    for (let i = 0; i < valor.length; i++) {
      binary += String.fromCharCode(valor[i]);
    }

    return btoa(binary);
  }

  private DesdeBase64(base64: string): Uint8Array {
    const binary = atob(base64);
    const bytes = new Uint8Array(binary.length);

    for (let i = 0; i < binary.length; i++) {
      bytes[i] = binary.charCodeAt(i);
    }

    return bytes;
  }

  private async ObtenerClaveFisica(clave: string): Promise<string> {
    const material = this.textEncoder.encode(`${this.secretoAplicacion}:${clave}`);
    const hash = await crypto.subtle.digest('SHA-256', material);
    const hashBytes = new Uint8Array(hash);

    return `${this.prefijoClave}${this.ConvertirHex(hashBytes)}`;
  }

  private ConvertirHex(valor: Uint8Array): string {
    let resultado = '';

    for (let i = 0; i < valor.length; i++) {
      resultado += valor[i].toString(16).padStart(2, '0');
    }

    return resultado;
  }
}
