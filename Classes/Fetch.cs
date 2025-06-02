using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Exiled.API.Features;

namespace SaskycStylesEasy.Classes
{
    public static class Fetch
    {
        public static Regex TagRegex = new(@"(?<tag>\w+)(\s*\((?<args>[^\)]*)\))?\s*\{(?<body>[^}]*)\}", RegexOptions.Compiled);
        public static Regex PropertyRegex = new(@"(?<key>\w+)\s*:\s*(?<value>[^;]+);", RegexOptions.Compiled);
        
        public static void IterateEachFile(string[] sseFiles)
        {
            foreach (var file in sseFiles)
            {
                Log.Debug($"\nParsing file: {Path.GetFileName(file)}");

                string content;
                try
                {
                    content = File.ReadAllText(file);
                }
                catch (Exception ex)
                {
                    Log.Error($"Could not read file {file}: {ex.Message}");
                    continue;
                }

                // Reverted regex: capture tagName, optional (args), and { body } up to the first }
                var tagMatches = TagRegex.Matches(content);

                
            }
        }
        
        public static void IterateEachTag(CollectionMatch tagMatches)
        {
            foreach (Match match in tagMatches)
                {
                    var tagName = match.Groups["tag"].Value.Trim();
                    var body = match.Groups["body"].Value.Trim();

                    // Parse the comma-separated argument list (if any)
                    var argsGroup = match.Groups["args"].Success ? match.Groups["args"].Value : "";
                    var argList = argsGroup
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(arg => arg.Trim())
                        .ToList();

                    if (string.IsNullOrWhiteSpace(body))
                    {
                        Log.Debug($"⚠️ Empty tag: {tagName}");
                        continue;
                    }

                    // Create a new Tag with an empty Dictionary<Property,string> and the parsed argList
                    var fetchedTag = new Tag(
                        tagName,
                        new Dictionary<Property, string>(),
                        argList
                    );

                    Log.Debug($"\n🔍 Validating tag: {tagName}");

                    // Find all key:value; pairs inside the tag body
                    
                    
                    
                    var propertyMatches = PropertyRegex.Matches(body);
                    foreach (Match propMatch in propertyMatches)
                    {
                        var key = propMatch.Groups["key"].Value.Trim();
                        var value = propMatch.Groups["value"].Value.Trim();

                        // Check if this key corresponds to a registered Property subclass
                        var registeredProp = Property.List
                            .FirstOrDefault(p => p.Name.Equals(key, StringComparison.OrdinalIgnoreCase));

                        if (registeredProp != null)
                        {
                            // Known "Property" → add or override in fetchedTag.Properties
                            if (fetchedTag.Properties.ContainsKey(registeredProp))
                                fetchedTag.Properties[registeredProp] = value;
                            else
                                fetchedTag.Properties.Add(registeredProp, value);
                        }
                        else
                        {
                            // Not a registered Property → treat as a local variable (x:100, etc.)
                            fetchedTag.Variables[key.ToLower()] = value;
                        }
                    }

                    // If a tag with the same name already exists, skip adding this one
                    if (Tag.List.Any(existing =>
                        existing.Name.Equals(fetchedTag.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        Log.Debug($"⚠️ Duplicate tag skipped: {fetchedTag.Name}");
                        continue;
                    }
                    // ** Dump out both Properties and Variables for this tag: **
                    Log.Debug($"(DEBUG) Tag “{fetchedTag.Name}” has:");
                    Log.Debug($"  Properties = [{string.Join(", ", fetchedTag.Properties.Keys.Select(p => $"{p.Name}: {fetchedTag.Properties[p]}"))}]");
                    Log.Debug($"  Variables  = [{string.Join(", ", fetchedTag.Variables.Select(kv => $"{kv.Key}: {kv.Value}"))}]");

                    Tag.List.Add(fetchedTag);
                }
        }

        public static void FetchAllPropertiesToTags()
        {
            var dllDirectory = Paths.Plugins;
            if (dllDirectory == null) return;

            var sseFolderPath = Path.Combine(dllDirectory, "SaskycStylesEasy");
            if (!Directory.Exists(sseFolderPath))
            {
                Log.Error("❌ Folder SaskycStylesEasy does not exist.");
                return;
            }

            var sseFiles = Directory.GetFiles(sseFolderPath, "*.sse", SearchOption.AllDirectories);

            // Clear out the existing tag list before repopulating
            Tag.List.Clear();

            IterateEachFile(sseFiles)
        }
    }
}
