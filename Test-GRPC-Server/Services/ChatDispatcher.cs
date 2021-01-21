
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGRPC.Server.Protos;

namespace TestGRPC.Server.Services
{
    public class ChatDispatcher
    {
        public delegate Task MessageReceivedHandler(string author, MessageBody message);

        private Dictionary<string, MessageReceivedHandler> registrations = new();

        public void Subscribe(string user, MessageReceivedHandler handler) => registrations.Add(user, handler);
        public void Unsuscribe(string user) => registrations.Remove(user);

        public async Task DispatchMessage(string user, MessageBody message)
        {
            IEnumerable<Task> handlingTasks = registrations
                .Where(reg => reg.Key != user)
                .Select(reg => reg.Value)
                .Select(handler => handler(user, message));

            await Task.WhenAll(handlingTasks);
        }
    }
}