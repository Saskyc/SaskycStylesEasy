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
    public override string[] needed_variables { get; set; } = [];
    public override int Priority { get; set; } = 8;

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        var childOutput = "";
        foreach (var tagsCompleted in TagComplete.Get(value))
        {
            Log.Debug($"    Processing additionTag string: \"{tagsCompleted.Tag.Name}\"");

            childOutput = Tag.ExecuteTag(
                player, tagsCompleted.Tag.Name,
                tagsCompleted.Arguments,
                out var start,
                out var content,
                out var end,
                defaultText: result
            );
        }

        return string.Empty;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        start = string.Empty;
        end = string.Empty;
    }
}
