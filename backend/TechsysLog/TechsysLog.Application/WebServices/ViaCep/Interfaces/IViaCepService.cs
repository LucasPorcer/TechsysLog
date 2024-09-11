using TechsysLog.Application.WebServices.ViaCep.Responses;

namespace TechsysLog.Application.WebServices.ViaCep.Interfaces
{
    public interface IViaCepService
    {
        Task<AddressResponseModel> GetAddressvViaCepAsync(string cep);
    }
}
