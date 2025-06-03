using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CommandSystem.Commands.RemoteAdmin.Dms;
using Exiled.API.Features;
using SaskycStylesEasy.Propertiess;
using YamlDotNet.Core.Tokens;

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
        
        public static Tag Get(string tagName)
        {
            return List.FirstOrDefault(x => x.Name == tagName);
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
        // <u> <i> <b>
        
        public List<int> processPriority()
        {
            List<int> priority = [];

            foreach (var property in Properties)
            {
                if (GetVariableValue($"{property.Key.Name}_priority", out string variable) is not null)
                    if (!int.TryParse(variable, out int parseResult))
                        Log.Error($"  Your priority setting of: {property.Key.Name} wasn't succsesful. Use numbers for priority 1 being lowest.");
                    else
                    {
                        property.Key.Priority = parseResult;
                        if (!priority.Contains(parseResult))
                        {
                            priority.Add(parseResult);
                            priority.Sort();
                        }
                    }
            }
            return priority;
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


            var normalStarts = new StringBuilder();
            var normalEnds = new StringBuilder();
            var inlineStarts = new StringBuilder();
            string innerText = defaultText;

            string encloseValue = null;

            // 3) Process each property of this tag
            Log.Info($"Tag: {foundTag.Name}");

            List<int> priorities = foundTag.processPriority();
            foreach(var priority in priorities)
            {
                foundTag.Properties.Where(x => x.Key.Priority == priority).ToList();
            }

            var result = "";
            foreach (var property in foundTag.Properties)
            {
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
                property.Key.Build(foundTag, value, normalStarts, inlineStarts, innerText, normalEnds);

                result = normalStarts.ToString()
                         + inlineStarts.ToString()
                         + innerText
                         + normalEnds.ToString();

                result = property.Key.Executing(result, value, foundTag, player);
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

        public string GetVariableValue(string parsedVariable, out string theVariable)
        {
            if (Variables.TryGetValue(parsedVariable.ToLower(), out var variable))
            {
                theVariable = variable;
                return variable;
            }

            theVariable = string.Empty;
            return null;
        }

        public string[] GetRawArguments(string[] rawArgs)
        {
            return rawArgs.Select(x => Variables.TryGetValue(x, out var variable) ? variable : x).ToArray();
        }
    }
}