using Exiled.API.Features;
using HintServiceMeow.Core.Enum;
using SaskycStylesEasy.Propertiess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.Core.Tokens;

namespace SaskycStylesEasy.Classes
{
    public abstract class Property
    {
        public abstract string Name { get; set; }
        public abstract string Start { get; set; }
        public abstract ValueType ParserValue { get; set; }
        public abstract string End { get; set; }

        public abstract int Priority { get; set; }

        public static List<Property> List = new();
        public abstract string[] needed_variables { get; set; }

        public static void RegisterAll()
        {
            var types = Assembly.GetCallingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(Property)))
                {
                    List.Add((Property)Activator.CreateInstance(type));
                }
            }
        }

        public bool HasNeededVariables(Tag tag)
        {
            return needed_variables.All(x => tag.GetVariableValue(x, out var variable) is not null);
        }

        public string Executing(string result, string value, Tag tag, Player player)
        {
            return result += Execute(tag, value, player, result);
        }

        public abstract string Execute(Tag tag, string value, Player player, string result);

        public void Build(Tag tag, string value, StringBuilder normalStarts, StringBuilder inlineStarts, string innerText, StringBuilder normalEnds)
        {
            Log.Info($"  Property: {Name}");

            if (ParserValue == ValueType.Boolean)
            {
                var boolText = value.ToLower();

                if (boolText == "false") return;
            }

            Process(tag, value, out string start, out string end);

            if (string.IsNullOrEmpty(end))
            {
                // e.g. property.Key.Start == "<alpha=%value%>"
                inlineStarts.Append(start);
                return;
            }

            // Otherwise, it’s a normal open/close tag
            normalStarts.Append(start);
            // Prepend the closing tag so that when all ends are concatenated, they close in reverse order
            normalEnds.Insert(0, end);
        }

        public abstract void Process(Tag tag, string value, out string start, out string end);

        public enum ValueType
        {
            String,
            Integer,
            Hex,
            AlignType,
            Boolean,
        }

        public enum AlignType
        {
            Left,
            Center,
            Right,
        }
    }
}