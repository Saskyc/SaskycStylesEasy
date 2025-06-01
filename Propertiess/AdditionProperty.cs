using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class AdditionProperty : Property
{
    public override string Name { get; set; } = "addition";
    public override string Start { get; set; } = string.Empty;
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = string.Empty;

    public string Add(Tag tag, Dictionary<string, string> localVariables, Player player, string result)
    {
        var tagsToAdd = Tag.SplitTopLevel(tag.Properties[this])
            .Where(t => !string.IsNullOrEmpty(t));

        foreach (var tagEntry in tagsToAdd)
        {
            Log.Debug($"    Processing additionTag string: \"{tagEntry}\"");
            var (tagName, rawArgs) = Tag.ParseTagWithArgs(tagEntry);

            var resolvedArgs = rawArgs
                .Select(a => localVariables.TryGetValue(a, out var variable) ? variable : a)
                .ToArray();

            var childOutput = Tag.ExecuteTag(
                player, tagName,
                resolvedArgs,
                out var start,
                out var content,
                out var end,
                defaultText: ""
            );
            
            Log.Info($"    RESULT BEFORE: {result}");
            result += " " + childOutput;
            Log.Info($"    RESULT AFTER: {result}");
        }
        
        return result;
    }
}

