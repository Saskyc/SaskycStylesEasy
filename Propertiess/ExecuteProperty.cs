using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class ExecuteProperty : Property
{
    public override string Name { get; set; } = "execute";

    public override string Start { get; set; } = "";
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = "";

    // Execute property method - input: arguments + text to process
    public virtual string Execute(string[] arguments, string text)
    {
        // By default, just return the text unchanged.
        return text;
    }
}
