export enum OrderStatusEnum {
  Aberto = 1,
  Enviado = 2,
  Entregue = 3,
  Cancelado = 4
}


export function getOrderStatusLabel(status: OrderStatusEnum): string {
  switch (status) {
    case OrderStatusEnum.Aberto:
      return 'Aberto';
    case OrderStatusEnum.Enviado:
      return 'Enviado';
    case OrderStatusEnum.Entregue:
      return 'Entregue';
    case OrderStatusEnum.Cancelado:
      return 'Cancelado';
    default:
      return 'Desconhecido';
  }
}