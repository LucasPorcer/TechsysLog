import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'; 
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { UsuarioModel } from '../models/usuario/usuarioModel';
import { PedidoModel } from '../models/pedido/pedidoModel';
import { AuthResponseModel } from './Responses/Auth/authResponse';
import { environment } from 'src/environments/environment';
import { LoginModel } from '../models/login/loginModel';
import { CreateOrderCommand } from '../models/Requests/createOrderRequest';
import { EntregaModel } from '../models/entrega/entregaModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = environment.API_URL;

  constructor(private http: HttpClient) {}

  // Headers com JWT
  private getHeaders() {
    const token = localStorage.getItem('jwt');
    return {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      })
    };
  }

  private saveToken(token: string) {
    localStorage.setItem('jwt', token);
  }

  // Autenticar usuário
  auth(credentials: LoginModel): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.apiUrl}/login`, credentials);
  }

  // Cadastrar usuário
  cadastrarUsuario(usuario: UsuarioModel): Observable<UsuarioModel> {
    return this.http.post<UsuarioModel>(`${this.apiUrl}/user`, usuario);
  }

  // Cadastrar pedido
  cadastrarPedido(pedido: CreateOrderCommand): Observable<PedidoModel> {
    return this.http.post<PedidoModel>(`${this.apiUrl}/order`, pedido, this.getHeaders())
      .pipe(catchError(error => this.handleUnauthorizedError(error)));
  }

  // Listar pedidos
  listarPedidosPaginados(page: number, size: number): Observable<any> {
    const params = {
      pageIndex: page.toString(),
      pageSize: size.toString()
    };

    return this.http.get<any>(`${this.apiUrl}/order`, { params, headers: this.getHeaders().headers })
      .pipe(catchError(error => this.handleUnauthorizedError(error)));
  }

   // Busca Entrega
  buscarEntrega(orderNumber: number): Observable<EntregaModel> {
    const params = {
      orderNumber: orderNumber
    };

    return this.http.get<EntregaModel>(`${this.apiUrl}/delivery`, { params, headers: this.getHeaders().headers })
      .pipe(catchError(error => this.handleUnauthorizedError(error)));
  }

  // Registrar entrega
  registrarEntrega(numeroPedido: number): Observable<PedidoModel> {
    return this.http.post<PedidoModel>(`${this.apiUrl}/order/delivery`, {number: numeroPedido} , this.getHeaders())
      .pipe(catchError(error => this.handleUnauthorizedError(error)));
  }

  // Buscar endereço pelo CEP
  buscarEndereco(cep: string): Observable<any> {
    const params = {
      cep: cep
    };

    return this.http.get<any>(`${this.apiUrl}/address`, { params, headers: this.getHeaders().headers })
    .pipe(catchError(error => this.handleUnauthorizedError(error)));
  }

  private handleUnauthorizedError(error: HttpErrorResponse): Observable<any> {
    if (error.status === 401) {
      return this.refreshToken().pipe(
        switchMap((response: AuthResponseModel) => {
          this.saveToken(response.jwt);
          const clonedRequest = error.url ? this.http.get(error.url, this.getHeaders()) : throwError(error);
          return clonedRequest;
        })
      );
    }
    return throwError(error);
  }

  private refreshToken(): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.apiUrl}/refreshToken`, {});
  }
}
