namespace WebApplication1.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detailed { get; set; }
        public Boolean Success { get; set; }
    }
}
