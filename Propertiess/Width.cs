using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Width : Property
{
    public override string Name { get; set; } = "width";
    public override string Start { get; set; } = "<width=%value%>";
    public override ValueType ParserValue { get; set; } = ValueType.Integer;
    public override string End { get; set; } = "</width>";
}