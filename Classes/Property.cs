using System;
using System.Collections.Generic;
using System.Reflection;

namespace SaskycStylesEasy.Classes
{
    public abstract class Property
    {
        public abstract string Name { get; set; }
        public abstract string Start { get; set; }
        public abstract ValueType ParserValue { get; set; }
        public abstract string End { get; set; }
        
        public static List<Property> List = new();
        
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