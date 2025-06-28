using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Request;
using Application.Request.SessionHub;
using Microsoft.AspNetCore.Http;
using Application.Response;
using Application.Interfaces.Services;

namespace Infrastructrure.HttpClients
{
    public class HistoryServiceClient : IHistoryServiceClient
    {
        private readonly HttpClient _httpClient;

        public HistoryServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterSlideChange(Guid SessionId,SlideSnapshotDto slide)
        {
            var jsonContent = JsonSerializer.Serialize(slide);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            //Console.WriteLine(await httpContent.ReadAsStringAsync());
            HttpResponseMessage response = await _httpClient.PostAsync($"api/History/{SessionId}/SlideChange", httpContent);
            return response;

        }
        public async Task<HttpResponseMessage> RecordAnswerHistory(AnswerRequest answer)
        {
            var jsonContent = JsonSerializer.Serialize(answer);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            Console.WriteLine(await httpContent.ReadAsStringAsync());

            HttpResponseMessage response = await _httpClient.PostAsync($"answer/", httpContent);
            return response;
        }

    }
}
