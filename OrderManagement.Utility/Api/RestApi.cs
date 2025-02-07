
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using OrderManagement.Model;

// ReSharper disable InconsistentNaming

namespace OrderManagement.Utility.Api
{
    public class RestApi
    {
        public async Task<string> Call<T>(string url, HttpMethod method, T? entity = null) where T : BaseDto
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                if (method == HttpMethod.Post)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, content);
                }
                else
                    response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Web Call Error," +
                                        Environment.NewLine + Environment.NewLine +
                                        "Status Code: " + response.StatusCode +
                                        Environment.NewLine +
                                        "Reason Phrase: " + response.ReasonPhrase);
                }
                var responseString = await response.Content.ReadAsStringAsync();
                
                return responseString;
            }
        }
    }
}
