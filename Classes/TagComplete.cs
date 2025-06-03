using Exiled.API.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;

namespace SaskycStylesEasy.Classes
{
    public class TagComplete
    {
        public Tag Tag { get; set; }
        public string[] Arguments { get; set; }

        public static Regex TagRegex = new(@"\w+(?:\([^()]*\))?", RegexOptions.Compiled);
        public static Regex TagArgumentsRegex = new(@"^(\w+)(?:\(([^)]*)\))?$", RegexOptions.Compiled);

        public TagComplete(Tag tag, string[] arguments)
        {
            Tag = tag;
            Arguments = arguments;
        }

        public static TagComplete[] Get(string value)
        {
            MatchCollection matches = TagRegex.Matches(value);
            TagComplete[] tagCompletes = [];

            for (int i = 0; i < matches.Count; i++)
            {
                string tagParsed = matches[i].Value;

                // Extract tag name and arguments
                Match tagMatch = TagArgumentsRegex.Match(tagParsed);

                if (!tagMatch.Success)
                {
                    Log.Error($"Invalid tag format: {tagParsed}");
                    continue;
                }

                string tagName = tagMatch.Groups[1].Value;

                string[] args = Array.Empty<string>();
                if (tagMatch.Groups[2].Success && !string.IsNullOrWhiteSpace(tagMatch.Groups[2].Value))
                {
                    args = tagMatch.Groups[2].Value
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < args.Length; j++)
                        args[j] = args[j].Trim();
                }

                // Get the Tag using the name only
                Tag? tag = Tag.Get(tagName);
                if (tag is not null)
                {
                    tagCompletes.AddItem(new TagComplete(tag, args));
                }
                else
                {
                    Log.Error($"Can't execute tag: {tagName} — it doesn't exist. Tried executing from tag: {tag.Name}");
                }
            }
            return tagCompletes;
        }
    }
}
