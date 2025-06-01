using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Italic : Property
{
    public override string Name { get; set; } = "italic";
    public override string Start { get; set; } = "<i>";
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</i>";
}