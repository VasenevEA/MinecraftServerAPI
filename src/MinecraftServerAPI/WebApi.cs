using Nancy;
using Nancy.Hosting.Self;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerAPI
{
    public class WebApi : NancyModule
    {
        private static Task serverTask;
        public WebApi()
        {
            Post["/setCommand", true] = async (ctx, ct) =>
             {
                 var command = GetObject<TransferCommand>();
                 Program.MCserver.StandardInput.WriteLine(command.command);
                 Console.WriteLine(command.command);
                 await Task.Delay(100);
                 return Program.lastMessage;
             };
        }

        public static void start(string url)
        {
            serverTask = Task.Factory.StartNew(async () =>
             {
                 using (var host = new NancyHost(new Uri(url)))
                 {
                     host.Start();

                     for (int i = 0; i < 40; i++)
                         Console.Write('*');
                     Console.WriteLine();
                     Console.WriteLine(url);
                     for (int i = 0; i < 40; i++)
                         Console.Write('*');
                     Console.WriteLine();

                     while (true)
                     {
                         await Task.Delay(1000);
                     }
                 }
             });
        }

        public static void stop()
        {
            if (serverTask != null)
                serverTask.Wait(1);
        }

        public T GetObject<T>()
        {
            var body = this.Request.Body;
            int length = (int)body.Length;
            byte[] data = new byte[length];
            body.Read(data, 0, length);

            return JsonConvert.DeserializeObject<T>(System.Text.Encoding.Default.GetString(data));
        }

        class TransferCommand
        {
            public string command { get; set; }
        }
    }
}
