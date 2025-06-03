using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Exiled.API.Features;
using HarmonyLib;
using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class AdditionProperty : Property
{
    public override string Name { get; set; } = "addition";
    public override string Start { get; set; } = string.Empty;
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = string.Empty;
    public override string[] needed_variables { get; set; } = [];
    public override int Priority { get; set; } = 5;

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        foreach (var tagsCompleted in TagComplete.Get(value))
        {
            Log.Debug($"    Processing additionTag string: \"{tagsCompleted.Tag.Name}\"");

            var childOutput = Tag.ExecuteTag(
                player, tagsCompleted.Tag.Name,
                tagsCompleted.Arguments,
                out var start,
                out var content,
                out var end,
                defaultText: ""
            );
            result += " " + childOutput;
        }
        
        return result;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        start = string.Empty;
        end = string.Empty;
    }
}

