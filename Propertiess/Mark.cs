using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess;

public class Mark : Property
{
    public override string Name { get; set; } = "mark";
    public override string Start { get; set; } = "<mark=%value% padding='%up%, %right%, %down%, %left%'>";
    public override ValueType ParserValue { get; set; } = ValueType.String;
    public override string End { get; set; } = "</mark>";
}