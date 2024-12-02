using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegisterationFlow.Application.Responses
{
    public  interface IBaseResponse
    {
        public int Code { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string SentDate { get; set; } 
          
    }
}
