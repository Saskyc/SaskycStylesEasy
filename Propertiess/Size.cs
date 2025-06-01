using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class Size : Property
{
    public override string Name { get; set; } = "size";
    public override string Start { get; set; } = "<size=%value%>";
    public override ValueType ParserValue { get; set; } = ValueType.Integer;
    public override string End { get; set; } = "</size>";
}