using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class Margin : Property
{
    public override string Name { get; set; } = "margin";
    public override string Start { get; set; } = "<margin=%value%em>";
    public override ValueType ParserValue { get; set; } = ValueType.Integer;
    public override string End { get; set; } = "</margin>";
}