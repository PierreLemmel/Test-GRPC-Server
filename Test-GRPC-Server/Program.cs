using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Test_GRPC_Server;


Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build()
    .Run();