using System;
using SaskycStylesEasy.Classes;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using YamlDotNet.Core;

namespace SaskycStylesEasy.Propertiess;

public class Enclose : Property
{
    public override string Name { get; set; } = "enclose";

    public override string Start { get; set; } = String.Empty;
    public override string End { get; set; } = String.Empty;
    
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override int Priority { get; set; } = 6;
    public override string[] needed_variables { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

            result = start + result + end;
        }

        return result;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        start = string.Empty;
        end = string.Empty;
    }
}