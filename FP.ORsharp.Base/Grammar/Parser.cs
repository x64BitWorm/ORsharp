using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Grammar
{
    public class Parser
    {
        private bool _debug;
        private string _language;
        private Dictionary<string, List<Rule>> _rules;

        public Parser(string language, bool debug = false)
        {
            _debug = debug;
            _language = language;
            ParseGrammar();
        }

        public Lexem Parse(string rule, string input)
        {
            foreach(var matchRule in _rules[rule])
            {
                foreach (var parsedRule in ParseRule(matchRule, input))
                {
                    if (parsedRule.result != null && parsedRule.nextInput.Length == 0)
                    {
                        parsedRule.result.Name = "ROOT";
                        return parsedRule.result;
                    }
                }
            }
            return null;
        }

        private IEnumerable<(Lexem result, string nextInput)> ParseRule(Rule rule, string input)
        {
            if(_debug)
            {
                Console.WriteLine($"'{input}' with {rule}");
            }
            var ruleResults = new List<IEnumerator<(Lexem result, string nextInput)>>();
            var inputResults = new List<string>();
            int symbolIndex = 0;
            nextSolution:
            while (symbolIndex < rule.Symbols.Count)
            {
                while (inputResults.Count > symbolIndex + 1)
                {
                    inputResults.RemoveAt(inputResults.Count - 1);
                }
                while (ruleResults.Count > symbolIndex + 1)
                {
                    ruleResults.RemoveAt(ruleResults.Count - 1);
                }
                if (inputResults.Count == symbolIndex)
                {
                    inputResults.Add(input);
                }
                input = inputResults[symbolIndex];
                var symbol = rule.Symbols[symbolIndex];
                IEnumerator<(Lexem result, string nextInput)> symbolResults;
                if (ruleResults.Count == symbolIndex)
                {
                    switch (symbol.Type)
                    {
                        case RuleSymbolType.Regex:
                            symbolResults = ParseRegex(symbol.Parameters[0], input).GetEnumerator();
                            break;
                        case RuleSymbolType.Pattern:
                            symbolResults = ParsePattern(symbol.Parameters[0], input).GetEnumerator();
                            break;
                        default:
                            throw new Exception("unknown rule found");
                    }
                    ruleResults.Add(symbolResults);
                }
                symbolResults = ruleResults[symbolIndex];
                while(symbolResults.MoveNext())
                {
                    var symbolResult = symbolResults.Current;
                    if (symbol.Name != "_")
                    {
                        symbolResult.result.Name = symbol.Name;
                    }
                    input = symbolResult.nextInput;
                    symbolIndex++;
                    goto nextSymbol;
                }
                if(symbolIndex == 0)
                {
                    yield break;
                }
                symbolIndex--;
                if (_debug)
                {
                    Console.WriteLine("going back");
                }
                nextSymbol:;
            }
            var result = new Lexem();
            for (int i = 0; i < rule.Symbols.Count; i++)
            {
                if (rule.Symbols[i].Name != "_")
                {
                    result.Childs.Add(rule.Symbols[i].Name, ruleResults[i].Current.result);
                }
                else
                {
                    foreach(var child in ruleResults[i].Current.result.Childs)
                    {
                        result.Childs.Add(child.Key, child.Value);
                    }
                }
            }
            yield return (result, input);
            symbolIndex--;
            goto nextSolution;
        }

        private IEnumerable<(Lexem result, string nextInput)> ParsePattern(string rule, string input)
        {
            if(_debug)
            {
                Console.WriteLine($"'{input}' finding rule '{rule}'");
            }
            foreach (var matchRule in _rules[rule])
            {
                foreach (var parsedRule in ParseRule(matchRule, input))
                {
                    if (parsedRule.result != null)
                    {
                        yield return parsedRule;
                    }
                }
            }
        }

        private IEnumerable<(Lexem result, string nextInput)> ParseRegex(string pattern, string input)
        {
            if(_debug)
            {
                Console.WriteLine($"'{input}' with regex '{pattern}'");
            }
            var regex = new Regex(pattern);
            var match = regex.Match(input);
            if(match.Success)
            {
                var result = new Lexem();
                foreach(var group in match.Groups.Keys)
                {
                    if(char.IsDigit(match.Groups[group].Name[0]))
                    {
                        continue;
                    }
                    result.Childs.Add(group, new Lexem { Name = match.Groups[group].Value });
                }
                yield return (result, input.Substring(match.Index + match.Length));
            }
        }

        private void ParseGrammar()
        {
            _rules = new Dictionary<string, List<Rule>>();
            foreach(var rule in _language.Split("\r\n"))
            {
                try
                {
                    if (rule.Length == 0 || rule.StartsWith("#"))
                    {
                        continue;
                    }
                    var ruleRegex = Utils.GetRegex("^(?<name>[^\\s]+)\\s+=\\s+(?<body>.*)$", rule);
                    var newRule = new Rule() { Name = ruleRegex["name"][0] };
                    var ruleSymbols = Utils.GetRegex("\\[(?<ruleArgs>.+?(?=](\\s|$)))\\]", ruleRegex["body"][0]);
                    foreach (var ruleSymbol in ruleSymbols["ruleArgs"])
                    {
                        var symbols = Utils.GetRegex("('(?<symbol_2>[^']+)')|(?<symbol_1>[^\\s]+)", ruleSymbol);
                        foreach (var symbol in symbols)
                        {
                            var newSymbol = new RuleSymbol()
                            {
                                Name = symbols["symbol"][0],
                                Type = RuleSymbol.GetTypeFromName(symbols["symbol"][1])
                            };
                            for (int i = 2; i < symbols["symbol"].Count; i++)
                            {
                                newSymbol.Parameters.Add(symbols["symbol"][i]);
                            }
                            newRule.Symbols.Add(newSymbol);
                        }
                    }
                    if (!_rules.ContainsKey(newRule.Name))
                    {
                        _rules.Add(newRule.Name, new List<Rule>());
                    }
                    _rules[newRule.Name].Add(newRule);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error while parsing grammar on line '{rule}'");
                    throw;
                }
            }
        }
    }
}
