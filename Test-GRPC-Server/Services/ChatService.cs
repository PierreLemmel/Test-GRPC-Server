using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGRPC.Server.Protos;
using static TestGRPC.Server.Protos.Chat;

namespace TestGRPC.Server.Services
{
    public class ChatService : ChatBase
    {
        private readonly ChatDispatcher dispatcher;

        public ChatService(ChatDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public override async Task Conversation(IAsyncStreamReader<SentMessage> requestStream, IServerStreamWriter<ReceivedMessage> responseStream, ServerCallContext context)
        {
            string user = context.RequestHeaders.GetValue("user");
            
            dispatcher.Subscribe(
                user,
                async (string author, MessageBody msg) => await responseStream.WriteAsync(new()
                {
                    Author = author,
                    Body = msg
                })
            );

            try
            {
                await foreach(SentMessage message in requestStream.ReadAllAsync())
                {
                    await dispatcher.DispatchMessage(user, message.Body);
                }
            }
            finally
            {
                dispatcher.Unsuscribe(user);
            }
        }
    }
}