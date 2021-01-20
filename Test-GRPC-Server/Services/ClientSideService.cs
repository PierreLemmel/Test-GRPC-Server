using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGRPC.Server.Protos;
using static TestGRPC.Server.Protos.ClientSide;

namespace TestGRPC.Server.Services
{
    public class ClientSideService : ClientSideBase
    {
        public override async Task<GatherResponse> Gather(IAsyncStreamReader<GatherRequest> requestStream, ServerCallContext context)
        {
            ICollection<string> messages = new List<string>();
            await foreach(GatherRequest request in requestStream.ReadAllAsync())
            {
                messages.Add(request.Msg);
            }

            GatherResponse response = new() { Result = string.Join(" - ", messages) };
            return response;
        }
    }
}