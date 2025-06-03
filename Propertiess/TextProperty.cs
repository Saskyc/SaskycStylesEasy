using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class TextProperty : Property
{
    public override string Name { get; set; } = "text";

    public override string Start { get; set; } = "";  // no start tag for content
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = "";    // no end tag for content

    public override int Priority { get; set; } = 1;
    public override string[] needed_variables { get; set; } = [];

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        return string.Empty;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        start = string.Empty;
        end = string.Empty;
    }
}
