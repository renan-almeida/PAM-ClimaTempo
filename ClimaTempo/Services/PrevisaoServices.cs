using ClimaTempo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class PrevisaoServices
    {
        private HttpClient client;
        private Previsao previsao;
        private Previsao previsaoProximosDias;
        private JsonSerializerOptions options;

        Uri uri = new Uri("https://brasilapi.com.br/api/cptec/v1/clima/previsao/");

        public PrevisaoServices() {
            client = new HttpClient();  
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<Previsao> GetPrevisaoById(int cityCode)
        {
            Uri requestUri = new Uri($"{uri}/{cityCode}");
            try
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsao = JsonSerializer.Deserialize<Previsao>(content, options);
                };
            }
            catch (Exception ex) { 
                Debug.WriteLine(ex.Message);
            }
            return previsao;
        }

        public async Task<Previsao> GetPrevisaoForXDaysById(int cityCode, int days)
        {
            try
            {
                cityCode = 244;
                days = 3;
                Uri requestUri = new Uri($"{uri}/{cityCode}/{days}");

                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsaoProximosDias = JsonSerializer.Deserialize<Previsao>(content, options);
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return previsaoProximosDias;
        }

    }
}
