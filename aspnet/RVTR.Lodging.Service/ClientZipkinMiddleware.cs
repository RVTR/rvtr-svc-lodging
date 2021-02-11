using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace RVTR.Lodging.Service
{
  internal class ClientZipkinMiddleware : IMiddleware
  {
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    public ClientZipkinMiddleware(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
      _configuration = configuration;
      _loggerFactory = loggerFactory;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      var lifetime = context.RequestServices.GetService<IHostApplicationLifetime>();
      var statistics = new Statistics();

      lifetime.ApplicationStarted.Register(() =>
      {
        var logger = new TracingLogger(_loggerFactory, "zipkin.aspnet");
        var sender = new HttpZipkinSender(_configuration.GetConnectionString("zipkin"), "application/json");
        var tracer = new ZipkinTracer(sender, new JSONSpanSerializer(), statistics);

        TraceManager.SamplingRate = 1.0f;
        TraceManager.Trace128Bits = true;
        TraceManager.RegisterTracer(tracer);
        TraceManager.Start(logger);
      });

      lifetime.ApplicationStopped.Register(() =>
      {
        TraceManager.Stop();
      });

      await next(context);
    }
  }
}
