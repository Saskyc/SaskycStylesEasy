using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt.Propertiess
{
    public class Align : Property
    {
        public override string Name { get; set; } = "align";
        public override string Start { get; set; } = "<align=%value%>";
        public override ValueType ParserValue { get; set; } = ValueType.String;
        public override string End { get; set; } = "</align>";
    }
}