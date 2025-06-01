using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Rotate : Property
{
    public override string Name { get; set; } = "rotate";
    public override string Start { get; set; } = "<rotate=%value%>";
    public override ValueType ParserValue { get; set; } = ValueType.Integer;
    public override string End { get; set; } = "</rotate>";
}