using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Italic : Property
{
    public override string Name { get; set; } = "italic";
    public override string Start { get; set; } = "<i>";
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</i>";

    public override int Priority { get; set; } = 1;
    public override string[] needed_variables { get; set; } = [];

    public override string Execute(Tag tag, string value, Player player, string result)
    {
        return string.Empty;
    }

    public override void Process(Tag tag, string value, out string start, out string end)
    {
        start = Start;
        end = End;
    }
}