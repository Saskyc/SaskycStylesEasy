using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Alpha : Property
{
    public override string Name { get; set; } = "alpha";
    public override string Start { get; set; } = "<alpha=%value%>";
    public override ValueType ParserValue { get; set; } = ValueType.Integer;
    public override string End { get; set; } = "";
}