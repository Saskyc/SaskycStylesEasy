using Exiled.API.Features;
using SaskycStylesEasy.Classes;
using UnityEngine;

namespace SaskycStylesEasy.Propertiess;

public class Rotate : Property
{
    public override string Name { get; set; } = "rotate";
    public override string Start { get; set; } = "<rotate=%value%>";
    public override ValueType ParserValue { get; set; } = ValueType.Integer;
    public override string End { get; set; } = "</rotate>";
    public override int Priority { get; set; } = 1;
    public override string[] needed_variables { get; set; } = [];

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        return string.Empty;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        tag.GetVariableValue("cspace", out string variable);

        start = Start.Replace("%value%", value);
        end = End;

        if (!variable.IsEmpty())
        {
            start = $"<cspace={variable}>{start}";
            end += "</cspace>";
        }
    }
}