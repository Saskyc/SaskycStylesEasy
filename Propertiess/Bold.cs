using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class Bold : Property
{
    public override string Name { get; set; } = "bold";
    public override string Start { get; set; } = "<b>";
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</b>";
}