using ConfigManager;
using System.Collections.Generic;

namespace ParserGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Config.LoadToClassFromFile<List<Rule>>("grammar.cfg");
        }
    }
}
