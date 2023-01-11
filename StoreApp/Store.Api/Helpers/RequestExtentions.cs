namespace Store.Api.Helpers
{
    public static class RequestExtentions
    {
        public static string BaseUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}
