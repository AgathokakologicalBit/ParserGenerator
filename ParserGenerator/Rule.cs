using ConfigManager;
using System.Collections.Generic;

namespace ParserGenerator
{
    public class Rule
    {
        [ConfigDataSource("$0")] public string Name;
        [ConfigDataSource("")] public List<Pattern> Patterns;
    }

    public class Pattern
    {
        [ConfigDataSource("$0")] public string Value;

        [ConfigDataSource("scope")] public string Scope;
        [ConfigDataSource("push.$0")] public string TransferScope;
        [ConfigDataSource("push.$1")] public string TransferMode;
    }
}
