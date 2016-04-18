using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dnd5eCharGen
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONConverter loader = new JSONConverter();
            CLI cli = new CLI();
            bool quit = false;
            while (!quit)
            {
                string command = Console.ReadLine();
                if (command != null && command != "quit")
                {
                    var results = from result in cli.Commands
                                  where Regex.Match(command, result.Key, RegexOptions.Singleline).Success
                                  select result;
                    if (results.Count() == 1)
                    {
                            results.First().Value.Invoke(command);
                    }else if (!results.Any())
                    {
                        Console.WriteLine("Not a valid command: " + command);
                    }
                    else
                    {
                        Console.WriteLine("Multiple possible commands, contact support!");
                    }
                    

                }else if (command == "quit")
                {
                    quit = true;
                }
            }

        }
    }
}
