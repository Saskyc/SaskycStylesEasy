using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Exiled.API.Features;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Utilities;
using SaskycStylesTestt.Classes;
using SaskycStylesTestt.Propertiess;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

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
        
        public static List<Tag> List = new List<Tag>();
        
        public Dictionary<string, string> Variables { get; set; } = new();
        
        public Tag(string name, Dictionary<Property, string> properties, List<string> args = null)
        {
            Name = name;
            Properties = properties;
            Arguments = args ?? new();
            Variables = new Dictionary<string, string>();
        }

        public static string ExecuteTag(Player player, string tag, string[] arguments, string text, Dictionary<string, string> localVariables = null)
        {
        Fetch.FetchAllPropertiesToTags();

        // 1) Find the tag
        if (List.All(x => x.Name != tag))
        {
            Log.Error($"Tag {tag} you tried to execute was not found.");
            return text;
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
    
        
        var start = "";
        var end = "";
        string innerText = text;

        string encloseValue = null;
        string additionalResult = "";
        ExecuteProperty executeProp = null;
        Show show = null;
        
        // 3) Process each property of this tag
        foreach (var property in foundTag.Properties)
        {
            Log.Info(property.Key.Name);
            
            var value = property.Value.Trim();

            // 1) Substitute tag‐arguments inside “value”
            for (int i = 0; i < foundTag.Arguments.Count && i < arguments.Length; i++)
            {
                var pattern = $@"\b{Regex.Escape(foundTag.Arguments[i])}\b";
                value = Regex.Replace(value, pattern, arguments[i]);
            }

            // 2) Substitute any “#var” placeholders from the current localVariables
            value = Regex.Replace(value, @"#(\w+)", match =>
            {
                var key = match.Groups[1].Value;
                return localVariables.TryGetValue(key, out var val) ? val : match.Value;
            });

            // 3) NOW: Bind THIS property’s value into localVariables so that
            //    future calls like “myOtherTag(color, bold)” can resolve “bold”:
            localVariables[property.Key.Name] = value;

            // … then continue with Boolean check, “enclose”, “execute”, etc.
            if (property.Key.ParserValue == Property.ValueType.Boolean)
            {
                var boolText = value.Trim().ToLower();
                if (boolText == "false") continue;
            }
            
            if (property.Key is Show showProperty)
            {
                Log.Info("At least found show");
                
                var showText = value.Trim().ToLower();
                if (showText == "false") continue;
                show = showProperty;
                continue;
            }

            if (property.Key is Mark)
            {
                Log.Debug($"EXECUTING MARK {property.Key.Start}");
                var up = "";
                var dow = "";
                var ri = "";
                var le = "";
                
                if(foundTag.Variables.ContainsKey("markUp"))
                    up = foundTag.Variables["markUp"];
                if(foundTag.Variables.ContainsKey("markDown"))
                    dow = foundTag.Variables["markDown"];
                if(foundTag.Variables.ContainsKey("markRight"))
                    ri = foundTag.Variables["markRight"];
                if(foundTag.Variables.ContainsKey("markLeft"))
                    le = foundTag.Variables["markLeft"];
                
                Log.Debug($"up: {up}, dow: {dow}, ri: {ri}, le: {le}");

                if (up == "" || dow == "" || ri == "" || le == "")
                    Log.Debug(
                        $"Please implement in your {foundTag.Name} the markUp, markDown, markRight, markLeft values");
                
                property.Key.Start = property.Key.Start.Replace("%up%", up);
                property.Key.Start = property.Key.Start.Replace("%down%", dow);
                property.Key.Start = property.Key.Start.Replace("%right%", ri);
                property.Key.Start = property.Key.Start.Replace("%left%", le);
                //property.Key.Start.Replace("%color%", result);
            }
            
            if (property.Key.Name.Equals("enclose", StringComparison.OrdinalIgnoreCase))
            {
                encloseValue = value;
                continue;
            }

            if (property.Key is ExecuteProperty execProp)
            {
                executeProp = execProp;
                continue;
            }

            if (property.Key is TextProperty)
            {
                innerText = value;
                continue;
            }

            start += property.Key.Start.Replace("%value%", value);
            end = property.Key.End + end;
        }

        // 4) Build the main output for this tag
        var result = start + innerText + end;
        
        // 5) Apply “enclose” (wrapping) if present
        if (!string.IsNullOrEmpty(encloseValue))
        {
            var enclosingTags = SplitTopLevel(encloseValue)
                .Where(t => !string.IsNullOrEmpty(t));

            Log.Debug($"Enclosing tags to apply: {string.Join(", ", enclosingTags)}");

            foreach (var enclosingTagEntry in enclosingTags)
            {
                Log.Debug($"Processing encloseTag string: \"{enclosingTagEntry}\"");
                var (tagName, rawArgs) = ParseTagWithArgs(enclosingTagEntry);

                var resolvedArgs = rawArgs
                    .Select(a => localVariables.ContainsKey(a) ? localVariables[a] : a)
                    .ToArray();

                Log.Debug($"→ Parsed enclosing: {tagName} with args: [{string.Join(", ", resolvedArgs)}]");
                var childOutput = ExecuteTag(
                    player, tagName,
                    resolvedArgs,
                    result,
                    new Dictionary<string, string>(localVariables)
                );

                Log.Debug($"→ Enclosing child \"{tagName}\" returned: \"{childOutput}\"");
                result = result + " " + childOutput;
                Log.Debug($"→ New result after enclosing: \"{result}\"");
            }
        }

        // 6) Apply “addition” if present
        if (foundTag.Properties.Any(p => p.Key is AdditionProperty))
        {
            foreach (var property in foundTag.Properties.Where(p => p.Key is AdditionProperty))
            {
                var tagsToAdd = SplitTopLevel(property.Value)
                    .Where(t => !string.IsNullOrEmpty(t));

                foreach (var tagEntry in tagsToAdd)
                {
                    Log.Debug($"Processing additionTag string: \"{tagEntry}\"");
                    var (tagName, rawArgs) = ParseTagWithArgs(tagEntry);

                    var resolvedArgs = rawArgs
                        .Select(a => localVariables.ContainsKey(a) ? localVariables[a] : a)
                        .ToArray();

                    var childOutput = ExecuteTag(
                        player, tagName,
                        resolvedArgs,
                        "",
                        new Dictionary<string, string>(localVariables)
                    );

                    Log.Debug($"→ Addition child \"{tagName}\" returned: \"{childOutput}\"");
                    additionalResult += " " + childOutput;
                    Log.Debug($"→ additionalResult is now: \"{additionalResult}\"");
                }
            }
        }

        if (show != null)
        {
            Log.Info("Showing hint");

            foreach (var property in foundTag.Properties.Where(p => p.Key is AdditionProperty))
            {
            }
            
            if (PlayerDisplay.Get(player).HasHint(foundTag.Variables["hintId"]))
                PlayerDisplay.Get(player).RemoveHint(PlayerDisplay.Get(player).GetHint(foundTag.Variables["hintId"]));
            
            var hint = new Hint()
            {
                Id = foundTag.Variables["hintId"],
                XCoordinate = float.Parse(foundTag.Variables["hintX"]),
                YCoordinate = float.Parse(foundTag.Variables["hintY"]),
                Text = result + additionalResult,
            };
            
            PlayerDisplay.Get(player).AddHint(hint);
            PlayerDisplay.Get(player).RemoveAfter(hint, float.Parse(foundTag.Variables["hintTime"]));
        }
        
        // 9) Finally, apply “execute” if present
        if (executeProp != null)
        {
            // Use SplitTopLevel instead of string.Split(',')
            var executeTags = SplitTopLevel(foundTag.Properties[executeProp])
                .Where(t => !string.IsNullOrEmpty(t));

            Log.Debug($"Splitting execute into tags: {string.Join(" | ", executeTags)}");

            foreach (var execTagEntry in executeTags)
            {
                Log.Debug($"Processing execTag string: \"{execTagEntry}\"");
                var (tagName, rawArgs) = ParseTagWithArgs(execTagEntry);

                // Resolve each rawArg via localVariables
                var resolvedArgs = rawArgs
                    .Select(a => localVariables.ContainsKey(a) ? localVariables[a] : a)
                    .ToArray();

                Log.Debug($"→ Parsed tag: {tagName} with resolved args: [{string.Join(", ", resolvedArgs)}]");

                // Execute child, then append
                var childOutput = ExecuteTag(
                    player, tagName,
                    resolvedArgs,
                    result,
                    new Dictionary<string, string>(localVariables)
                );

                Log.Debug($"→ Child \"{tagName}\" returned: \"{childOutput}\"");
                result = result + " " + childOutput;
                Log.Debug($"→ New result after appending: \"{result}\"");
            }
        }

        return result + additionalResult;
        }
        
        private static List<string> SplitTopLevel(string input)
        {
            var parts = new List<string>();
            if (string.IsNullOrEmpty(input)) return parts;

            var sb = new StringBuilder();
            int depth = 0;

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
        
        private static (string tagName, string[] args) ParseTagWithArgs(string input)
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