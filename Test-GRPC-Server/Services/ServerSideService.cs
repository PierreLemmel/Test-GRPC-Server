using Grpc.Core;
using System.Threading.Tasks;
using TestGRPC.Server.Protos;
using static TestGRPC.Server.Protos.ServerSide;

namespace TestGRPC.Server.Services
{
    public class ServerSideService : ServerSideBase
    {
        public override async Task Count(CountRequest request, IServerStreamWriter<CountResponse> responseStream, ServerCallContext context)
        {
            for (int i = request.From; i < request.To; i++)
            {
                await responseStream.WriteAsync(new() { Current = i });
                await Task.Delay(1000);
            }
        }
    }
}