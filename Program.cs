﻿using System;
using Divine.CLI;

namespace Divine
{
    internal class Program
    {
        public static CommandLineArguments argv;

        private static void Main(string[] args)
        {
            var parser = new CommandLineParser.CommandLineParser
            {
                IgnoreCase = true,
                ShowUsageOnEmptyCommandline = true
            };

            argv = new CommandLineArguments();

            parser.ExtractArgumentAttributes(argv);

            try
            {
                parser.ParseCommandLine(args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"[FATAL] {e.Message}");
            }

            if (parser.ParsingSucceeded)
            {
                CommandLineActions.Run(argv);
            }
        }
    }
}
