
using Microsoft.AspNetCore.Http;

namespace CustomerRegisterationFlow.Application.Responses
{
 
    public class BaseCommandResponse : IBaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; } = true;
        public string SentDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public object Payload { get; set; }
        public int Id { get; set; } = 0;
    }
}

