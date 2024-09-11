using Microsoft.AspNetCore.SignalR;

namespace TechsysLog.Application.Hubs.Orders
{
    public class OrdersHub : Hub
    {   
        public async Task NotificarAtualizacaoPedidos()
        {
            await Clients.All.SendAsync("AtualizarPedidos");
        }

        public async Task NotificarListarPedidos()
        {
            await Clients.All.SendAsync("ListarPedidos");
        }
    }
}
