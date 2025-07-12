using System.Net.Http.Json;
using Application.Response;
using Application.Interfaces.Services;

namespace Infrastructrure.HttpClients
{
    public class PresentationServiceClient : IPresentationServiceClient
    {

        private readonly HttpClient _httpClient;

        public PresentationServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PresentationResponseDTO> GetPresentationByIdAsync(int id)
        {
            //nombre del metodo en el endpoint en el otro ms ?
            var response = await _httpClient.GetAsync($"api/v1/presentation/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadFromJsonAsync<PresentationResponseDTO>();
                return respuesta;
            }

            return null;
        }
    }
}