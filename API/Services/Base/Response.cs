namespace API.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
