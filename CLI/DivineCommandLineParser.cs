using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLineParser.Arguments;

namespace Divine.CLI
{
    public class DivineCommandLineParser : CommandLineParser.CommandLineParser
    {
        private static string WordWrap(string text, int maxLength = 80, int indent = 0)
        {
            string indentation = "".PadLeft(indent);

            var lineWrap = new Regex($"(.{{1,{maxLength}}})(?: +|$)");

            return lineWrap.Replace(text, $"$1\n{indentation}");
        }

        private static void AddFormattedArgument(Argument argument, ICollection<string> list, string newline)
        {
            string line = string.Empty;

            if (argument.ShortName.HasValue && !string.IsNullOrWhiteSpace(argument.LongName))
                line = $"  -{argument.ShortName}, --{argument.LongName}";
            else if (argument.ShortName.HasValue)
                line = $"  -{argument.ShortName}";
            else if (!string.IsNullOrWhiteSpace(argument.LongName))
                line = $"  --{argument.LongName}";

            if (!string.IsNullOrWhiteSpace(line) && !string.IsNullOrWhiteSpace(argument.Description))
            {
                // manually adjust to: longest length of `line` + `offset` = 80
                const int offset = 35;
                list.Add(newline + line.PadRight(line.Length + Math.Abs(line.Length - offset)) + argument.Description);
            }
            else
                list.Add(newline + line);

            if (argument is EnumeratedValueArgument<string> enumValueArg)
            {
                if (!string.IsNullOrWhiteSpace(enumValueArg.DefaultValue))
                    list.Add($"    Default Value:{newline}      {enumValueArg.DefaultValue}");

                string allowedValues = string.Join(", ", enumValueArg.AllowedValues);
                string formattedAllowedValues = WordWrap(allowedValues, 76, 6).TrimEnd();

                list.Add($"    Allowed Values:{newline}      {formattedAllowedValues}");
            }
        }

        public new void PrintUsage(TextWriter outputStream)
        {
            var lines = new List<string>();

            string newline = outputStream.NewLine;

            if (!string.IsNullOrWhiteSpace(ShowUsageHeader))
            {
                lines.Add("-------------------------------------------------------------------------------");
                lines.Add(ShowUsageHeader);
                lines.Add("-------------------------------------------------------------------------------");
            }

            lines.Add(newline + "required arguments:");

            foreach (Argument argument in Arguments.Where(argument => argument is { Optional: false }))
                AddFormattedArgument(argument, lines, newline);

            lines.Add(newline + "optional arguments:");

            foreach (Argument argument in Arguments.Where(argument => argument is { Optional: true }))
                AddFormattedArgument(argument, lines, newline);

            if (!string.IsNullOrWhiteSpace(ShowUsageFooter))
                lines.Add(ShowUsageFooter);

            outputStream.Write(string.Join(newline, lines));
            outputStream.WriteLine();
        }
    }
}