using MediatR;
using TechsysLog.Application.WebServices.ViaCep.Responses;

namespace TechsysLog.Application.Commands.Address.GetAddress
{
    public class GetAddressByCepCommand : IRequest<AddressResponseModel>
    {
        public string Cep { get; set; }
    }
}
