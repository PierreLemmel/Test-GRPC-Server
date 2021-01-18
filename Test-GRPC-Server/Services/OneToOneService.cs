using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGRPC.Server.Protos;

namespace TestGRPC.Server.Services
{
    public class OneToOneService : OneToOne.OneToOneBase
    {
        public override Task<AddNumbersResponse> AddNumbers(AddNumbersRequest request, ServerCallContext context)
        {
            AddNumbersResponse response = new() { Result = request.Lhs + request.Rhs };
            return Task.FromResult(response);
        }
    }
}