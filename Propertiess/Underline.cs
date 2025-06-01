using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class Underline : Property
{
    public override string Name { get; set; } = "underline";
    public override string Start { get; set; } = "<u>";
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</u>";
}