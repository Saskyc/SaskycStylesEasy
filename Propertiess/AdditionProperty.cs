using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class AdditionProperty : Property
{
    public override string Name { get; set; } = "addition";
    public override string Start { get; set; } = string.Empty;
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = string.Empty;
}

