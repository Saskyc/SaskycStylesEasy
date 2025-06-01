using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Exiled.API.Features;
using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class ExecuteProperty : Property
{
    public override string Name { get; set; } = "execute";
    
    public static Regex Regex = new(@"^(\w+)(?:\(([^)]*)\))?$", RegexOptions.Compiled);
    
    public override string Start { get; set; } = "";
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = "";

    // Execute property method - input: arguments + text to process
    public virtual void Execute(Tag tag, Dictionary<string, string> localVariables, Player player, string result)
    {
        // Use SplitTopLevel instead of string.Split(',')
        var executeTags = Tag.SplitTopLevel(tag.Properties[this])
            .Where(t => !string.IsNullOrEmpty(t));

        Log.Debug($"    Splitting execute into tags: {string.Join(" | ", executeTags)}");

        foreach (var execTagEntry in executeTags)
        {
            Log.Debug($"    Processing execTag string: \"{execTagEntry}\"");
            var (tagName, rawArgs) = Tag.ParseTagWithArgs(execTagEntry);

            // Resolve each rawArg via localVariables
            var resolvedArgs = rawArgs
                .Select(a => localVariables.TryGetValue(a, out var variable) ? variable : a)
                .ToArray();

            Log.Debug($"    → Parsed tag: {tagName} with resolved args: [{string.Join(", ", resolvedArgs)}]");

            // Execute child, then append
            var childOutput = Tag.ExecuteTag(
                player, tagName,
                resolvedArgs,
                out var start,
                out var content,
                out var end,
                defaultText: result
            );
            
            Log.Info($"    RESULT BEFORE: {result}");
            result = result + " " + childOutput;
            Log.Info($"    RESULT AFTER {result}");
        }
    }
}
