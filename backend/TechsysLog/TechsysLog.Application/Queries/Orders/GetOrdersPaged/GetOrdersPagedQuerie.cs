using MediatR;
using TechsysLog.Application.Dtos.Pagination;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Application.Queries.Orders.GetOrdersPaged
{
    public class GetOrdersPagedQuerie : IRequest<PagedResult<Order>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
