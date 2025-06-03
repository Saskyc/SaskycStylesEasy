using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Exiled.API.Features;
using SaskycStylesEasy.Propertiess;

namespace SaskycStylesEasy.Classes
{
    public class Tag
    {
        public string Name { get; set; }
        
        /// <summary>
        /// Dictionary which stores the properties with their string values
        /// </summary>
        public Dictionary<Property, string> Properties { get; set; }
        
        public List<string> Arguments { get; set; } = new();
        
        public static List<Tag> List = new();

        public string Path { get; set; }

        public Dictionary<string, string> Variables { get; set; } = new();
        
        public Tag(string name, Dictionary<Property, string> properties, List<string> args = null)
        {
            Name = name;
            Properties = properties;
            Arguments = args ?? new();
            Variables = new Dictionary<string, string>();
        }

        public object HasProperty<T>(string[] arguments, Dictionary<string, string> localVariables = null)
        {
            localVariables ??= new Dictionary<string, string>();
            
            foreach (var property in Properties)
            {
                var value = property.Value.Trim();
            
                for (int i = 0; i < this.Arguments.Count && i < arguments.Length; i++)
                {
                    var pattern = $@"\b{Regex.Escape(this.Arguments[i])}\b";
                    value = Regex.Replace(value, pattern, arguments[i]);
                }
            
                value = Regex.Replace(value, @"#(\w+)", match =>
                {
                    var key = match.Groups[1].Value;
                    return localVariables.TryGetValue(key, out var val) ? val : match.Value;
                });

            
                localVariables[property.Key.Name] = value;

                if (property.Key is not T myProperty) continue;
                
                if (property.Key.ParserValue != Property.ValueType.Boolean) return myProperty;
                
                var boolText = value.Trim().ToLower();
                if (boolText == "false") return null;

                return myProperty;
            }
            
            return null;
        }
        
        public static string ExecuteTag(Player player, string tag, string[] arguments, out string startTags, out string content, out string endTags, string defaultText = "", Dictionary<string, string> localVariables = null)
        {
            //Fetch.FetchAllPropertiesToTags();
            startTags = string.Empty;
            content = string.Empty;
            endTags = string.Empty;
            
            // 1) Find the tag
            if (List.All(x => x.Name != tag))
            {
                Log.Error($"Tag {tag} you tried to execute was not found.");
                return defaultText;
            }

            localVariables ??= new Dictionary<string, string>();

            var foundTag = List.Find(x => x.Name == tag);

            // 2) Bind incoming arguments into localVariables for this tag
            for (int i = 0; i < foundTag.Arguments.Count && i < arguments.Length; i++)
            {
                // arguments[i] might be "#whatever" or a literal—store it under its argument name
                localVariables[foundTag.Arguments[i]] = arguments[i];
            }
        
            // 2b) Bind tag‐level variables (e.g. “x: 100;”) into localVariables
            foreach (var kv in foundTag.Variables)
            {
                localVariables[kv.Key] = kv.Value;
            }
    
        
            var normalStarts   = new StringBuilder();
            var normalEnds     = new StringBuilder();
            var inlineStarts   = new StringBuilder();
            string innerText   = defaultText;

            string encloseValue = null;
        
            // 3) Process each property of this tag
            Log.Info($"Tag: {foundTag.Name}");
            foreach (var property in foundTag.Properties)
            {   
                Log.Info($"  Property: {property.Key.Name}");
            
                var value = property.Value.Trim();
            
                for (int i = 0; i < foundTag.Arguments.Count && i < arguments.Length; i++)
                {
                    var pattern = $@"\b{Regex.Escape(foundTag.Arguments[i])}\b";
                    value = Regex.Replace(value, pattern, arguments[i]);
                }
            
                value = Regex.Replace(value, @"#(\w+)", match =>
                {
                    var key = match.Groups[1].Value;
                    return localVariables.TryGetValue(key, out var val) ? val : match.Value;
                });

            
                localVariables[property.Key.Name] = value;
            
                if (property.Key.ParserValue == Property.ValueType.Boolean)
                {
                    var boolText = value.Trim().ToLower();
                    if (boolText == "false") continue;
                }
            
                if (property.Key is Show showProperty)
                {
                    var showText = value.Trim().ToLower();
                    if (showText == "false") continue;
                    continue;
                }

                if (property.Key is Rotate)
                {
                    var cspace = "0";
                    if(foundTag.Variables.TryGetValue("cspace", out var variable))
                        cspace = variable;
                    property.Key.Start = $"<cspace={cspace}>{property.Key.Start}";
                    property.Key.End = $"{property.Key.End}</cspace>";
                }

                if (property.Key is Mark)
                {
                    Log.Debug($"EXECUTING MARK {property.Key.Start}");
                    var left = "";
                    var right = "";
                    var up = "";
                    var down = "";
                
                    if(foundTag.Variables.TryGetValue("markup", out var variable))
                        left = variable;
                    if(foundTag.Variables.TryGetValue("markdown", out var tagVariable))
                        right = tagVariable;
                    if(foundTag.Variables.TryGetValue("markright", out var foundTagVariable))
                        up = foundTagVariable;
                    if(foundTag.Variables.TryGetValue("markleft", out var variable1))
                        down = variable1;
                
                    Log.Debug($"left: {left}, right: {right}, up: {up}, down: {down}");

                    if (left == "" || right == "" || up == "" || down == "")
                        Log.Debug(
                            $"Please implement in your {foundTag.Name} the markUp, markDown, markRight, markLeft values");
                
                    property.Key.Start = property.Key.Start.Replace("%up%", left);
                    property.Key.Start = property.Key.Start.Replace("%down%", right);
                    property.Key.Start = property.Key.Start.Replace("%right%", up);
                    property.Key.Start = property.Key.Start.Replace("%left%", down);
                    //property.Key.Start.Replace("%color%", result);
                }
            
                if (property.Key.Name.Equals("enclose", StringComparison.OrdinalIgnoreCase))
                {
                    encloseValue = value;
                    continue;
                }

                if (property.Key is ExecuteProperty execProp)
                {
                    continue;
                }

                if (property.Key is TextProperty)
                {
                    innerText = value;
                    continue;
                }
            
                // If this is alpha (or any Property whose End == ""), collect it as “inline”
                if (string.IsNullOrEmpty(property.Key.End))
                {
                    // e.g. property.Key.Start == "<alpha=%value%>"
                    inlineStarts.Append(property.Key.Start.Replace("%value%", value));
                    continue;
                }
            
                // Otherwise, it’s a normal open/close tag
                normalStarts.Append(property.Key.Start.Replace("%value%", value));
                // Prepend the closing tag so that when all ends are concatenated, they close in reverse order
                normalEnds.Insert(0, property.Key.End);
            }

            // 4) Build the main output for this tag
            startTags = normalStarts.ToString() + inlineStarts.ToString();
            content = innerText;
            endTags = normalEnds.ToString();
            
            var result = normalStarts.ToString()
                         + inlineStarts.ToString()
                         + innerText
                         + normalEnds.ToString();
        
            if (foundTag.HasProperty<AdditionProperty>(arguments, localVariables) != null)
            {
                var addition = (AdditionProperty)foundTag.HasProperty<AdditionProperty>(arguments, localVariables);
                result = addition.Add(foundTag, localVariables, player, result);
            }
        
            if (foundTag.HasProperty<Enclose>(arguments, localVariables) != null)
            {
                var enclose = (Enclose)foundTag.HasProperty<Enclose>(arguments, localVariables);
                result = enclose.Enclosen(foundTag, localVariables, player, result);
            }
        
            if (foundTag.HasProperty<Show>(arguments, localVariables) != null)
            {
                var show = (Show)foundTag.HasProperty<Show>(arguments, localVariables);
                show.Hint(foundTag, player, result);
            }
        
            if(foundTag.HasProperty<ExecuteProperty>(arguments, localVariables) != null)
            {
                var executeProperty = (ExecuteProperty)foundTag.HasProperty<ExecuteProperty>(arguments, localVariables);
                executeProperty.Execute(foundTag, localVariables, player, result);
            }

            return result;
        }
        
        public static List<string> SplitTopLevel(string input)
        {
            var parts = new List<string>();
            if (string.IsNullOrEmpty(input)) return parts;

            var sb = new StringBuilder();
            var depth = 0;

            foreach (char c in input)
            {
                if (c == '(')
                {
                    depth++;
                    sb.Append(c);
                }
                else if (c == ')')
                {
                    depth = Math.Max(0, depth - 1);
                    sb.Append(c);
                }
                else if (c == ',' && depth == 0)
                {
                    // At depth=0, this comma separates top-level segments
                    parts.Add(sb.ToString().Trim());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }

            // Add whatever remains
            parts.Add(sb.ToString().Trim());
            return parts;
        }
    
        public static (string tagName, string[] args) ParseTagWithArgs(string input)
        {
            var match = Regex.Match(input, @"^(\w+)(?:\(([^)]*)\))?$");
            if (!match.Success)
            {
                Log.Debug($"Invalid tag format: {input}");
                return (input, Array.Empty<string>());
            }

            var tagName = match.Groups[1].Value;
            var args = match.Groups[2].Success
                ? match.Groups[2].Value.Split(',').Select(a => a.Trim()).ToArray()
                : Array.Empty<string>();

            return (tagName, args);
        }
    }
}