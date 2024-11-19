using LogDBContext;

namespace RomanNumeralConverterAPI.Utilities
{
    public class LogEntryGenerator
    {
        public static RMLoggingModel GetModelFromContext(HttpContext context, string result)
        {
            var method = context.Request.Method;
            var url = context.Request.Path;

            // Get the source IP address
            var ipAddress = context.Connection.RemoteIpAddress;

            // Convert IP to a readable string (handles IPv6 or IPv4)
            var clientIp = ipAddress != null ? ipAddress.ToString() : "Unknown";

            var logModel = new RMLoggingModel
            {
                CreatedDate = DateTime.UtcNow,
                Request = $"Method: {method} : Path {url}",
                IpAddress = clientIp,
                Result = result
            };

            return logModel;

        }
    }
}

