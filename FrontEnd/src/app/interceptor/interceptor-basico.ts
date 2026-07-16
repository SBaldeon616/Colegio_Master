import { HttpErrorResponse, HttpEvent, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, from, map, mergeMap, throwError } from 'rxjs';
import { AlertaService, TokenSesionService } from '../shared/services';

type GeneralResponse = {
    success?: boolean;
    Success?: boolean;
    title?: string;
    Title?: string;
    message?: string;
    Message?: string;
    showAlert?: boolean;
    ShowAlert?: boolean;
    content?: unknown;
    Content?: unknown;
};

const RUTA_LOGIN = '/api/auth';

const ObtenerMensajeErrorBackend = (errorResponse: HttpErrorResponse): string => {
    if (typeof errorResponse.error === 'string') {
        return errorResponse.error;
    }

    if (errorResponse.error && typeof errorResponse.error === 'object') {
        const errorBody = errorResponse.error as Record<string, unknown>;

        if (typeof errorBody['message'] === 'string') {
            return errorBody['message'];
        }

        if (typeof errorBody['title'] === 'string') {
            return errorBody['title'];
    }
    }

    return errorResponse.message || '';
};

const EsGeneralResponse = (responseBody: unknown): responseBody is GeneralResponse => {
    if (!responseBody || typeof responseBody !== 'object') {
        return false;
    }

    const body = responseBody as Record<string, unknown>;

    return (
        'content' in body
        || 'Content' in body
        || 'showAlert' in body
        || 'ShowAlert' in body
        || 'success' in body
        || 'Success' in body
    );
};

const MostrarAlertaGeneralResponse = (alertaService: AlertaService, responseBody: GeneralResponse): void => {
    const showAlert = responseBody.showAlert ?? responseBody.ShowAlert;

    if (!showAlert) {
        return;
    }

    const title = responseBody.title ?? responseBody.Title ?? '';
    const message = responseBody.message ?? responseBody.Message ?? '';
    const success = responseBody.success ?? responseBody.Success;

    if (success) {
        void alertaService.MostrarSuccess(title, message);
        return;
    }

    void alertaService.MostrarWarning(title, message);
};

const AgregarTokenSiCorresponde = (request: Parameters<HttpInterceptorFn>[0], token: string | null) => {
    if (request.headers.has('Authorization') || request.url.includes(RUTA_LOGIN)) {
        return request;
    }

    if (!token) {
        return request;
    }

    return request.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`
        }
    });
};

const DesempaquetarGeneralResponse = (eventoHttp: HttpEvent<unknown>, alertaService: AlertaService): HttpEvent<unknown> => {
    if (!(eventoHttp instanceof HttpResponse) || !EsGeneralResponse(eventoHttp.body)) {
        return eventoHttp;
    }

    MostrarAlertaGeneralResponse(alertaService, eventoHttp.body);

    const contenido = eventoHttp.body.content ?? eventoHttp.body.Content;

    if (contenido === undefined || contenido === null) {
        return eventoHttp;
    }

    return eventoHttp.clone({
        body: contenido
    });
};

export const interceptorBasico: HttpInterceptorFn = (request, next) => {
    const alertaService = inject(AlertaService);
    const tokenSesionService = inject(TokenSesionService);

    const EjecutarRequest = (requestConHeaders: Parameters<HttpInterceptorFn>[0]) => next(requestConHeaders).pipe(
        map((eventoHttp: HttpEvent<unknown>) => DesempaquetarGeneralResponse(eventoHttp, alertaService)),
        catchError((errorResponse: unknown) => {
            if (errorResponse instanceof HttpErrorResponse) {
                void alertaService.MostrarErrorHttp(
                    errorResponse.status,
                    ObtenerMensajeErrorBackend(errorResponse)
                );
            }

            return throwError(() => errorResponse);
        })
    );

    if (request.headers.has('Authorization') || request.url.includes(RUTA_LOGIN)) {
        return EjecutarRequest(request);
    }

    return from(tokenSesionService.ObtenerToken()).pipe(
        map((token) => AgregarTokenSiCorresponde(request, token)),
        mergeMap((requestConHeaders) => EjecutarRequest(requestConHeaders))
    );
};
