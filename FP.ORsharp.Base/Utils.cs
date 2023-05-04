using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FP.ORsharp.Base
{
    public static class Utils
    {
        public static Dictionary<string, List<string>> GetRegex(string pattern, string input)
        {
            var result = new Dictionary<string, List<string>>();
            var regex = new Regex(pattern);
            var matches = regex.Matches(input);
            foreach(var match in matches.ToArray())
            {
                foreach(var group in match.Groups.Keys)
                {
                    if(match.Groups[group].Success)
                    {
                        var groupName = group;
                        if(char.IsDigit(groupName[0]))
                        {
                            continue;
                        }
                        if(groupName.Contains('_'))
                        {
                            groupName = groupName.Substring(0, groupName.LastIndexOf('_'));
                        }
                        if(!result.ContainsKey(groupName))
                        {
                            result.Add(groupName, new List<string>());
                        }
                        result[groupName].Add(match.Groups[group].Value);
                    }
                }
            }
            return result;
        }
    }
}
