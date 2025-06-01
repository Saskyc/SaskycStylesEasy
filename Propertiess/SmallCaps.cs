using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class SmallCaps : Property
{
    public override string Name { get; set; } = "smallcaps";

    public override string Start { get; set; } = "<smallcaps>";  // no start tag for content
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</smallcaps>";    // no end tag for content
}
