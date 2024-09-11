using MediatR;
using TechsysLog.Application.WebServices.ViaCep.Interfaces;
using TechsysLog.Application.WebServices.ViaCep.Responses;

namespace TechsysLog.Application.Commands.Address.GetAddress
{
    public class GetAddressByCepCommandHandler : IRequestHandler<GetAddressByCepCommand, AddressResponseModel>
    {
        private readonly IViaCepService _viaCepService;

        public GetAddressByCepCommandHandler(IViaCepService viaCepService)
        {
            _viaCepService = viaCepService;
        }

        public async Task<AddressResponseModel> Handle(GetAddressByCepCommand command, CancellationToken cancellationToken)
        {
            var addressResponse = await _viaCepService.GetAddressvViaCepAsync(command.Cep);

            if (addressResponse is null)
            {
                return null;
            }
           
            return addressResponse;
        }
    }
}
