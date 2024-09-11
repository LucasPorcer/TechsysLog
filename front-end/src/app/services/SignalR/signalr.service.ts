import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  private pedidoAtualizadoSource = new Subject<void>();
  pedidoAtualizado$ = this.pedidoAtualizadoSource.asObservable();

  private listarPedidosSource = new Subject<void>();
  listarPedidosSource$ = this.listarPedidosSource.asObservable();

  iniciarConexao() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.SIGNALR_URL, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets 
      })
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Conexão com SignalR estabelecida.'))
      .catch(err => console.error('Erro ao conectar com SignalR:', err));

    this.hubConnection.on('AtualizarPedidos', () => {
      this.pedidoAtualizadoSource.next();
    });

    this.hubConnection.on('ListarPedidos', () => {
      this.listarPedidosSource.next();
    });
  }

  pararConexao() {
    if (this.hubConnection) {
      this.hubConnection.stop().then(() => {
        console.log('Conexão com SignalR encerrada.');
      }).catch(err => {
        console.error('Erro ao encerrar a conexão com SignalR:', err);
      });
    } else {
      console.warn('Tentativa de parar uma conexão que não foi inicializada.');
    }
  }
  
}
