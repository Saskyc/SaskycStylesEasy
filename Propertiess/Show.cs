using System;
using System.Linq;
using Exiled.API.Features;
using SaskycStylesEasy.Classes;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Utilities;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace SaskycStylesEasy.Propertiess;

public class Show : Property
{
    public override string Name { get; set; } = "show";
    public override string Start { get; set; } = string.Empty;
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = string.Empty;
    public override int Priority { get; set; } = 7;
    public override string[] needed_variables { get; set; } = ["hintid", "hintx", "hinty", "hintduration"];

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        if (!HasNeededVariables(tag))
        {
            var errorText = ($"Implement in your tag: {tag.Name} these variables: ");
            needed_variables.ForEach(x => errorText += x + " ");
            Log.Error(errorText);
        }

        tag.GetVariableValue("hintid", out string hintId);
        tag.GetVariableValue("hintx", out string hintX);
        tag.GetVariableValue("hinty", out string hintY);
            
        if (PlayerDisplay.Get(player).HasHint(hintId))
            PlayerDisplay.Get(player).RemoveHint(PlayerDisplay.Get(player).GetHint(hintId));

        var hint = new Hint()
        {
            Id = tag.Variables["hintid"],
            XCoordinate = float.Parse(tag.Variables["hintx"]),
            YCoordinate = float.Parse(tag.Variables["hinty"]),
            Text = result,
        };
            
        PlayerDisplay.Get(player).AddHint(hint);
        PlayerDisplay.Get(player).RemoveAfter(hint, float.Parse(tag.Variables["hintduration"]));

        return result;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        start = string.Empty;
        end = string.Empty;
    }
}