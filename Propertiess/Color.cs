using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess
{
    public class Color : Property
    {
        public override string Name { get; set; } = "color";
        public override string Start { get; set; } = "<color=%value%>";
        public override ValueType ParserValue { get; set; } = ValueType.Hex;
        public override string End { get; set; } = "</color>";
    }
}