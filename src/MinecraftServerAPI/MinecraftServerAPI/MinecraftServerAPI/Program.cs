using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = loadConfig("config.txt");

            StreamWriter inputStream = null;
            StreamReader outputStream = null;

            startMinecraftServer(conf.path,conf.args, ref inputStream, ref outputStream);

            //
            while (!outputStream.EndOfStream)
            {
                string line = outputStream.ReadLine();
                Console.WriteLine(line);
            }
        }



        private static void startMinecraftServer(string path,string args, ref StreamWriter inputStream, ref StreamReader outputStream)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            inputStream = proc.StandardInput;
            outputStream = proc.StandardOutput;
        }

        private static Config loadConfig(string path)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }

        class Config
        {
            public string path { get; set; }
            public string args { get; set; }
        }
    }
}
