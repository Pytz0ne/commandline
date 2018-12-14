using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace commandline
{
    class Program
        {
            public string Header { get; set; }
            public List<string> Verbs;
        }
        static void Main(string[] args)
        {
            Command bufferComm = new Command() { Verbs = new List<string>() };
            List<Command> Commands = new List<Command>();
            Func<string, bool> AddCommand = buffer =>
            {
                if (new Regex(@"^--").IsMatch(buffer))
                {
                    if (bufferComm.Header != null)
                    {
                        Commands.Add(bufferComm);
                        bufferComm = new Command() { Verbs = new List<string>() };
                    }
                    return true;
                }
                return false;
            };
            foreach (var item in args)
            {
                if (AddCommand(item))
                    bufferComm.Header = item;
                else
                    bufferComm.Verbs.Add(item);
            }
            AddCommand("--");


            foreach (var comm in Commands)
                Console.WriteLine($"Header:{comm.Header}\nVerbs:{String.Join(" ", comm.Verbs)}");
            Console.ReadKey();
        }
    }

}
