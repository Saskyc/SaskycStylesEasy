using System;
using SaskycStylesEasy.Classes;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;

namespace SaskycStylesEasy.Propertiess;

public class Enclose : Property
{
    public override string Name { get; set; } = "enclose";

    public override string Start { get; set; } = String.Empty;
    public override string End { get; set; } = String.Empty;
    
    public override ValueType ParserValue { get; set; } = ValueType.String;

    public string Enclosen(Tag tag, Dictionary<string, string> localVariables, Player player, string result)
    {
        var enclosingTags = Tag.SplitTopLevel(tag.Properties[this])
            .Where(t => !string.IsNullOrEmpty(t));

        var childOutput = "";
        foreach (var enclosingTagEntry in enclosingTags)
        {
            var (tagName, rawArgs) = Tag.ParseTagWithArgs(enclosingTagEntry);

            var resolvedArgs = rawArgs
                .Select(a => localVariables.TryGetValue(a, out var variable) ? variable : a)
                .ToArray();
            
            childOutput = Tag.ExecuteTag(
                player, tagName,
                resolvedArgs,
                out var start,
                out var content,
                out var end,
                defaultText: result
            );
            
            result = start + result + end;
        }
        
        Console.WriteLine("------------------------------");
        Console.WriteLine($"    RESULT F: {result}");
        return result;
    }
}