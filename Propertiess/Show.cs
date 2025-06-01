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
    public override string Start { get; set; } = String.Empty;
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = String.Empty;

    public void Hint(Tag tag, Player player, string text)
    {
        foreach (var property in tag.Properties.Where(p => p.Key is AdditionProperty))
        {
        }
            
        if (PlayerDisplay.Get(player).HasHint(tag.Variables["hintid"]))
            PlayerDisplay.Get(player).RemoveHint(PlayerDisplay.Get(player).GetHint(tag.Variables["hintid"]));
            
        var hint = new Hint()
        {
            Id = tag.Variables["hintid"],
            XCoordinate = float.Parse(tag.Variables["hintx"]),
            YCoordinate = float.Parse(tag.Variables["hinty"]),
            Text = text,
        };
            
        PlayerDisplay.Get(player).AddHint(hint);
        PlayerDisplay.Get(player).RemoveAfter(hint, float.Parse(tag.Variables["hintduration"]));
    }
}