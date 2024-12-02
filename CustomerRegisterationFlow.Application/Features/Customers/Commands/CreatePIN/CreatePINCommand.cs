using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegisterationFlow.Application.Features.Customers.Commands.CreatePIN
{
    public class CreatePINCommand : IRequest<IBaseResponse>
    {
        public CreatePINDto CreatePINDto { get; set; }
    }
}
