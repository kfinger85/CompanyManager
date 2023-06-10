using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace CompanyManager.Logging
{
    public class ClientIpEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientIpEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var ip = httpContext.Connection.RemoteIpAddress.ToString();
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ClientIP", ip));
            }
        }
    }
}
