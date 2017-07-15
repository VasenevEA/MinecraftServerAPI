using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftServerAPI
{
    class Program
    {
        public static Process MCserver;
        public static string lastMessage;
        static void Main(string[] args)
        {
            var conf = loadConfig("config.txt");

            MCserver = startMinecraftServer(conf.path, conf.args);
            MCserver.Start();

            

            //start web server
            WebApi.start(conf.url);


            var readStream = MCserver.StandardOutput;
           // writer(MCserver.StandardInput);

            while (true)
            {
                string line = readStream.ReadLine();
                if (!String.IsNullOrEmpty(line))
                {
                    lastMessage = line;
                    Console.WriteLine(line);
                }

                Thread.Sleep(100);
            }
        }

        static void writer(StreamWriter inputStream)
        {
            Task.Factory.StartNew(async () =>
            {
                int i = 0;
                while (true)
                {
                    inputStream.WriteLine("/setblock " + i +  " 80 0 stone");

                    i++;
                    //inputStream.WriteLine(Console.ReadLine());
                    await Task.Delay(100);
                }
            });
        }



        private static Process startMinecraftServer(string path, string args)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "java",
                    Arguments = @"-jar " + path,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                }
            };

            return proc;
        }

        private static Config loadConfig(string path)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }

        class Config
        {
            public string path { get; set; }
            public string args { get; set; }
            public string url { get; set; }
        }
    }
}
