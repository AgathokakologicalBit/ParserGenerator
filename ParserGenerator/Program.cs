using ConfigManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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

            using (var f = File.OpenWrite("parser.cpp"))
            {
                WriteString(f, "#include <iostream>\n");
                WriteString(f, "#include <cstddef>\n");
                WriteString(f, "#include <string>\n");
                WriteString(f, "#include <vector>\n");

                WriteString(f,
                    "\n" +
                    "\n" +
                    "enum class ExpressionType {\n    " +
                         String.Join(",\n    ", rules.Keys.Select(k => "ET_" + k.ToUpperInvariant())) +
                    ",\n" +
                    "    ET_EOF,\n" +
                    "};\n"
                );

                WriteString(f,
                    "\n" +
                    "class Match final {\n" +
                    "public:\n" +
                    "    std::string value;\n" +
                    "    std::vector<Match  *> groups;\n" +
                    "};\n"
                );

                WriteString(f,
                    "\n" +
                    "class Expression final {\n" +
                    "public:\n" +
                    "    const ExpressionType type;\n" +
                    "    Match value;\n" +
                    "\n" +
                    "    explicit Expression (ExpressionType _type)\n" +
                    "        : type(_type)\n" +
                    "        , value()\n" +
                    "    {{}}" +
                    "};\n"
                );

                foreach (var kv in rules)
                {
                    WriteString(f,
                        $"\n" +
                        $"/** Parses @code{{{kv.Key.ToUpperInvariant()}}}\n" +
                        $" *    using the following pattern:\n" +
                        $" *    @code{{{kv.Value.Patterns[0].Value}}}\n" +
                        $"**/\n" +
                        $"Expression * parse_{kv.Key} ( ) {{" +
                        $"}};\n"
                    );
                }
            }
        }

        private static void WriteString(FileStream f, string s)
        {
            var bytes = Encoding.ASCII.GetBytes(s);
            f.Write(bytes, 0, bytes.Length);
        }
    }
}
