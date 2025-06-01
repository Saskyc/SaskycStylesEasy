using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class AllCaps : Property
{
    public override string Name { get; set; } = "allcaps";

    public override string Start { get; set; } = "<allcaps>";  // no start tag for content
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</allcaps>";    // no end tag for content
}
