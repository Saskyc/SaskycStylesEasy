using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class Indent : Property
{
    public override string Name { get; set; } = "indent";
    public override string Start { get; set; } = "<indent=%value%>";
    public override ValueType ParserValue { get; set; } = ValueType.Integer;
    public override string End { get; set; } = "</indent>";
}