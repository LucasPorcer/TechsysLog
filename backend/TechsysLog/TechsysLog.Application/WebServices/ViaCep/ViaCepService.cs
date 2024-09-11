using Microsoft.Extensions.Configuration;
using RestSharp;
using Polly;
using TechsysLog.Application.WebServices.ViaCep.Interfaces;
using TechsysLog.Application.WebServices.ViaCep.Responses;

namespace TechsysLog.Application.WebServices.ViaCep
{
    public class ViaCepService : IViaCepService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public ViaCepService(IConfiguration configuration)
        {
            _baseUrl = configuration["ViaCep:BaseUrl"];
        }

        public async Task<AddressResponseModel> GetAddressvViaCepAsync(string cep)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest($"{cep}/json", Method.Get);

          
            var retryPolicy = Policy
                .Handle<Exception>()  
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(3),
                (exception, timeSpan, retryCount, context) =>
                {
                    
                    Console.WriteLine($"Trye numer {retryCount} failed. Trying again at {timeSpan.TotalSeconds} seconds...");
                });

            // Executa a requisição dentro da política de retry
            return await retryPolicy.ExecuteAsync(async () =>
            {
                var response = await client.ExecuteAsync<AddressResponseModel>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    throw new Exception("Não foi possível buscar o endereço.");
                }
            });
        }
    }
}
