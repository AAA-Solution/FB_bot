using System.Text;
using Common;

namespace FB_Connector
{
    public class HttpClientRequest
    {
        public BaseResponse<T> PostRequest<T>(string api_url, Dictionary<string, string> header, string data, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, api_url);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");
            if (header != null)
            {
                foreach (var item in header)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            request.Content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var res = response.Content.ReadAsStringAsync().Result;

            return res.ToDeserializeCamels<BaseResponse<T>>();
        }
        public void PostRequest(string api_url, Dictionary<string, string> header, string data, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, api_url);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");
            if (header != null)
            {
                foreach (var item in header)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            request.Content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            //var res = response.Content.ReadAsStringAsync().Result;
        }
        public async Task<BaseResponse<T>> PostRequestAsync<T>(string api_url, Dictionary<string, string> header, string data, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, api_url);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");
            if (header != null)
            {
                foreach (var item in header)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            request.Content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();

            return res.ToDeserializeCamels<BaseResponse<T>>();
        }
        public BaseResponse<T> GetRequest<T>(string api_url, Dictionary<string, string> header, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, api_url);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");
            if (header != null)
            {
                foreach (var item in header)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var res = response.Content.ReadAsStringAsync().Result;

            return res.ToDeserializeCamels<BaseResponse<T>>();
        }

        public async Task<BaseResponse<T>> GetRequestAsync<T>(string api_url, Dictionary<string, string> header, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, api_url);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Add("Authorization", $"Bearer {token}");
            if (header != null)
            {
                foreach (var item in header)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();

            return res.ToDeserializeCamels<BaseResponse<T>>();
        }
    }
}
