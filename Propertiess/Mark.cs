using Exiled.API.Features;
using SaskycStylesEasy.Classes;
using System.Collections.Generic;

namespace SaskycStylesEasy.Propertiess;

public class Mark : Property
{
    public override string Name { get; set; } = "mark";
    public override string Start { get; set; } = "<mark=%value% padding='%markup%, %markright%, %markdown%, %markleft%'>";
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = "</mark>";
    public override string[] needed_variables { get; set; } = ["markup", "markdown", "markleft", "markright"];
    public override int Priority { get; set; } = 1;

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        return string.Empty;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        Log.Debug($"EXECUTING MARK {Start}");
        start = string.Empty;
        end = string.Empty;

        if (!HasNeededVariables(tag))
        {
            Log.Error($"Tag: {tag} did not have all needed variables for mark to function correctly.");
            return;
        }

        start = Start.Replace("%value%", value);

        foreach (var needed_variable in needed_variables)
            start = start.Replace($"%{needed_variable}%", tag.GetVariableValue(needed_variable, out string theVariable));

        end = End;
    }
}