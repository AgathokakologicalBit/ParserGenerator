using ConfigManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ParserGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            
            var rules = Config
                .LoadToClassFromFile<List<Rule>>("grammar.cfg")
                .ToDictionary(
                    v => v.Name,
                    v => v
                );
        }
    }
}
