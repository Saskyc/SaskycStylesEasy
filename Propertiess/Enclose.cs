using System;
using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class Enclose : Property
{
    public override string Name { get; set; } = "enclose";

    public override string Start { get; set; } = String.Empty;
    public override string End { get; set; } = String.Empty;
    
    public override ValueType ParserValue { get; set; } = ValueType.String;

    // Optionally, you can have extra logic here if needed later
}