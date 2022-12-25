namespace OrangeHRMTestFramework.HttpClients
{
    public class BasicHttpClient
    {
        private static HttpClient _httpClient;
        private static HttpRequestMessage _httpRequestMessage;
        private static HttpResponseMessage _restResponse;

        public static HttpResponseMessage PerformGetRequest(string requestUrl, Dictionary<string, string> headers) =>
            SendRequest(requestUrl, HttpMethod.Get, null, headers);

        private static HttpClient CreateHttpClientANdAddHeaders(Dictionary<string, string> headers)
        {
            var httpClient = new HttpClient();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return httpClient;
        }

        private static HttpRequestMessage CreateHttpRequestMessage(string requestUrl, HttpMethod httpMethod,
            HttpContent httpContent)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = httpMethod;
            httpRequestMessage.RequestUri = new Uri(requestUrl);

            if (httpMethod != HttpMethod.Get)
            {
                httpRequestMessage.Content = httpContent;
            }

            return httpRequestMessage;
        }

        private static HttpResponseMessage SendRequest(string requestUrl, HttpMethod httpMethod, HttpContent httpContent, Dictionary<string, string> headers)
        {
            _httpClient = CreateHttpClientANdAddHeaders(headers);
            _httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);
            var httpResponse = _httpClient.SendAsync(_httpRequestMessage).Result;

            try
            {
                _restResponse = new HttpResponseMessage();
            }
            catch (Exception e)
            {
                throw new Exception($"Error with response: Status code: {httpResponse.StatusCode}, \nException: {e.Message}");
            }
            finally
            {
                _httpRequestMessage?.Dispose();
                _httpClient?.Dispose();
            }

            return _restResponse;
        }
    }
}
