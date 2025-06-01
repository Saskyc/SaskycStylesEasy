using System;
using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess;

public class Show : Property
{
    public override string Name { get; set; } = "show";
    public override string Start { get; set; } = String.Empty;
    public override ValueType ParserValue { get; set; } = ValueType.Boolean;
    public override string End { get; set; } = String.Empty;
}