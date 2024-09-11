import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { PageEvent } from '@angular/material/paginator';
import { PedidoModel } from 'src/app/models/pedido/pedidoModel';
import { getOrderStatusLabel, OrderStatusEnum } from 'src/app/enums/OrderStatusEnum';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { SignalRService } from 'src/app/services/SignalR/signalr.service';
import { MatDialog } from '@angular/material/dialog';
import { EnderecoModalComponent } from '../../endereco/modals/endereco-modal.component';
import { Router } from '@angular/router';
import { EntregaModalComponent } from '../../entrega/modals/entrega-modal.component';

@Component({
  selector: 'app-pedido-list',
  templateUrl: './pedido-list.component.html',
  styleUrls: ['./pedido-list.component.css']
})
export class PedidoListComponent implements OnInit, OnDestroy {
  pedidos: PedidoModel[] = [];
  displayedColumns: string[] = ['number', 'description', 'value', 'status', 'visualizarEndereco', 'acao', 'visualizarEntrega'];
  totalPedidos: number = 0;
  pageSize: number = 10;
  pageIndex: number = 1;
  pedidoAtualizadoSub!: Subscription;
  listarPedidosSub!: Subscription;
  public OrderStatusEnum = OrderStatusEnum;

  constructor(
    private apiService: ApiService,
    private snackBar: MatSnackBar,
    private signalRService: SignalRService,
    private dialog: MatDialog,
   private router: Router
  ) {}

  ngOnInit() {
    this.listarPedidos(1,15);
    this.StartSignalR();
  }

  private StartSignalR() {
    this.signalRService.iniciarConexao();

    this.pedidoAtualizadoSub = this.signalRService.pedidoAtualizado$.subscribe(() => {
      this.listarPedidos(this.pageIndex, this.pageSize);
    });

    this.listarPedidosSub = this.signalRService.listarPedidosSource$.subscribe(() => {
      this.listarPedidos(this.pageIndex, this.pageSize);
    });
  }

  listarPedidos(pageIndex: number, pageSize: number) {
    this.apiService.listarPedidosPaginados(pageIndex, pageSize).subscribe(res => {
      this.pedidos = res.data;
      this.totalPedidos = res.total;
    });
  }

  paginacao(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.listarPedidos(this.pageIndex, this.pageSize);
  }

  marcarComoEntregue(pedido: PedidoModel) {
    this.apiService.registrarEntrega(pedido.number).subscribe({
      next: (res) => {
        pedido = res;
        this.snackBar.open('Pedido marcado como entregue com sucesso', 'Fechar', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center'
        });
        pedido.status = OrderStatusEnum.Entregue;

        // Forçar a atualização na lista de pedidos (caso o pedido esteja em uma lista)
        this.pedidos = this.pedidos.map(p => p.number === pedido.number ? { ...p, status: OrderStatusEnum.Entregue } : p);

        //Redirecionar
        this.router.navigate(['/pedidos']);
      },
      error: () => {
        this.snackBar.open('Não foi possível marcar como entregue', 'Fechar', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center'
        });
      }
    });
  }

  visualizarEndereco(pedido: PedidoModel) {
    
    this.dialog.open(EnderecoModalComponent, {
      data: pedido.orderAddress,
      width: '500px'
    });
  }

  visualizarEntrega(pedido: PedidoModel) {
    this.apiService.buscarEntrega(pedido.number).subscribe({
      next: (res) => {
        this.dialog.open(EntregaModalComponent, {
          data: res,
          width: '500px'
        });
      },
      error: () => {
        this.snackBar.open('Não foi possível visualizar a entrega deste pedido', 'Fechar', {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center'
        });
      }
    });
  }
  

  getStatusClass(status: OrderStatusEnum) {
    switch (status) {
      case OrderStatusEnum.Aberto:
        return 'status-aberto';
      case OrderStatusEnum.Enviado:
        return 'status-enviado';
      case OrderStatusEnum.Entregue:
        return 'status-entregue';
      case OrderStatusEnum.Cancelado:
        return 'status-cancelado';
      default:
        return '';
    }
  }

  getOrderStatusLabel(status: OrderStatusEnum): string {
    return getOrderStatusLabel(status); 
  }

  ngOnDestroy() {
    if (this.pedidoAtualizadoSub) {
      this.pedidoAtualizadoSub.unsubscribe();
    }
    this.signalRService.pararConexao();
  }
}
