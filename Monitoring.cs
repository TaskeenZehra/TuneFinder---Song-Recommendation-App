using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Song_Recommendation_App
{
    internal static class Monitoring
    {
        private static Guid _operationId;
        private static Stopwatch stopwatch;

        public static void InitializeLogging()
        {
            _operationId = Guid.NewGuid(); 
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\app_log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("=== Application Starting ===");
            Log.Information("Trace Operation ID: {OperationId}", _operationId);
        }

        public static void ShutdownLogging()
        {
            Log.Information("=== Application Closing ===");
            Log.CloseAndFlush();
        }

        public static void StartNewOperation()
        {
            _operationId = Guid.NewGuid();
            stopwatch = Stopwatch.StartNew();
            Log.Information("== New Operation Started: {OperationId} ==", _operationId);
        }

        public static void EndOperation(string operationName)
        {
            if (stopwatch != null && stopwatch.IsRunning)
            {
                stopwatch.Stop();
                Log.Information("Operation '{Operation}' completed in {Duration} ms. [ID: {OperationId}]",
                    operationName, stopwatch.ElapsedMilliseconds, _operationId);
            }
        }

        public static void LogInfo(string message)
        {
            Log.Information("[{OperationId}] {Message}", _operationId, message);
        }

        public static void LogWarning(string message)
        {
            Log.Warning("[{OperationId}] {Message}", _operationId, message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            if (ex != null)
                Log.Error(ex, "[{OperationId}] {Message}", _operationId, message);
            else
                Log.Error("[{OperationId}] {Message}", _operationId, message);
        }

        public static Guid GetCurrentOperationId()
        {
            return _operationId;
        }

        
        public static async Task<bool> CheckSpotifyHealth(string accessToken)
        {
            StartNewOperation(); 

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response = await client.GetAsync("https://api.spotify.com/v1/me");

                    if (response.IsSuccessStatusCode)
                    {
                        LogInfo("✅ Spotify API Health Check Passed.");
                        return true;
                    }
                    else
                    {
                        LogWarning($"⚠️ Spotify API returned status code: {response.StatusCode}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("❌ Spotify API Health Check Failed.", ex);
                return false;
            }
            finally
            {
                EndOperation("Spotify Health Check");
            }
        }
    }
}
