using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int x = ExecuteAsync(null).Result;
        }

        public static async Task<int> ExecuteAsync(string baseUrl)
        {
            var uri = baseUrl == null ? new Uri("http://127.0.0.1:64349/chat") : new Uri(baseUrl);
            Console.WriteLine("Connecting to {0}", uri);
            var connectionBuilder = new HubConnectionBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });


            connectionBuilder.WithUrl(uri);


            var connection = connectionBuilder.Build();

            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                connection.DisposeAsync().GetAwaiter().GetResult();
            };

            //NoticeOnline事件
            connection.On<string>("NoticeOnline", Console.WriteLine);
            //Send事件
            connection.On<string>("Send", Console.WriteLine);

            //断开链接事件
            CancellationTokenSource closedTokenSource = null;
            connection.Closed += e =>
            {
                // This should never be null by the time this fires
                closedTokenSource.Cancel();

                Console.WriteLine("Connection closed...");
                return Task.CompletedTask;
            };

            while (true)
            {
                // Dispose the previous token
                closedTokenSource?.Dispose();

                // Create a new token for this run
                closedTokenSource = new CancellationTokenSource();

                // Connect to the server
                if (!await ConnectAsync(connection))
                {
                    break;
                }

                Console.WriteLine("Connected to {0}", uri);

                // Handle the connected connection
                while (true)
                {
                    try
                    {
                        var line = Console.ReadLine();

                        if (line == null || closedTokenSource.Token.IsCancellationRequested)
                        {
                            break;
                        }

                        await connection.InvokeAsync<object>("Send", line);
                    }
                    catch (IOException)
                    {
                        // Process being shutdown
                        break;
                    }
                    catch (OperationCanceledException)
                    {
                        // The connection closed
                        break;
                    }
                    catch (ObjectDisposedException)
                    {
                        // We're shutting down the client
                        break;
                    }
                    catch (Exception ex)
                    {
                        // Send could have failed because the connection closed
                        System.Console.WriteLine(ex);
                        break;
                    }
                }
            }

            return 0;
        }
        private static async Task<bool> ConnectAsync(HubConnection connection)
        {
            // Keep trying to until we can start
            while (true)
            {
                try
                {
                    await connection.StartAsync();
                    return true;
                }
                catch (ObjectDisposedException)
                {
                    // Client side killed the connection
                    return false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to connect, trying again in 5000(ms)");

                    await Task.Delay(5000);
                }
            }
        }
    }
}
