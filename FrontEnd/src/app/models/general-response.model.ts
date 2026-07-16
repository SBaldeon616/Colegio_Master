export class GeneralResponse<T> {
    success: boolean = false;
    title: string = '';
    message: string = '';
    content: T | null = null;
    showAlert: boolean = false;
}