using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class LowerCase : Property
{
    public override string Name { get; set; } = "lowercase";

    public override string Start { get; set; } = "<lowercase>";  // no start tag for content
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = "</lowercase>";    // no end tag for content
}
