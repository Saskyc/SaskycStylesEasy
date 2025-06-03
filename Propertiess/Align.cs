using Exiled.API.Features;
using SaskycStylesEasy.Classes;

namespace SaskycStylesEasy.Propertiess
{
    public class Align : Property
    {
        public override string Name { get; set; } = "align";
        public override string Start { get; set; } = "<align=%value%>";
        public override ValueType ParserValue { get; set; } = ValueType.String;
        public override string End { get; set; } = "</align>";
        public override int Priority { get; set; } = 1;
        public override string[] needed_variables { get; set; } = [];

        public override string Execute(Tag tag, string value, Player player, string result)
        {
            return string.Empty;
        }

        public override void Process(Tag tag, string value, out string start, out string end)
        {
            start = Start.Replace("%value%", value);
            end = End;
        }
    }
}