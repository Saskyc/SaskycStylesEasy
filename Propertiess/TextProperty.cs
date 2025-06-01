using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class TextProperty : Property
{
    public override string Name { get; set; } = "text";

    public override string Start { get; set; } = "";  // no start tag for content
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = "";    // no end tag for content

    // New method to process text inside the tag, could be identity by default
    public virtual string ProcessText(string input)
    {
        return input; // Default: return input unchanged
    }
}
