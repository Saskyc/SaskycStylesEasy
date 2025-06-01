using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class UpperCase : Property
{
    public override string Name { get; set; } = "uppercase";

    public override string Start { get; set; } = "<uppercase>";  // no start tag for content
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</uppercase>";    // no end tag for content
}
