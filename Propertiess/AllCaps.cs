global using Exiled.API.Features;
using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class AllCaps : Property
{
    public override string Name { get; set; } = "allcaps";

    public override string Start { get; set; } = "<allcaps>";  // no start tag for content
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</allcaps>";    // no end tag for content

    public override int Priority { get; set; } = 1;
    public override string[] needed_variables { get; set; } = [];

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        return string.Empty;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        start = Start.Replace("%value%", value);
        end = End;
    }
}
