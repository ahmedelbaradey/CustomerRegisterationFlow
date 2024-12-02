using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegisterationFlow.Application.Responses
{

    public class ErrorDetails : IBaseResponse
    {
        public Guid RequestId { get; set ; }
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }=false; 
        public string SentDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public List<string> Errors { get; set; }
        public int Id { get; set; } = 0;
    }
}
