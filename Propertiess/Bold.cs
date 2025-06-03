using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Bold : Property
{
    public override string Name { get; set; } = "bold";
    public override string Start { get; set; } = "<b>";
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</b>";

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