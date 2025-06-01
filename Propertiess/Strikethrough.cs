using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Strikethrough : Property
{
    public override string Name { get; set; } = "strikethrough";
    public override string Start { get; set; } = "<s>";
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</s>";
}