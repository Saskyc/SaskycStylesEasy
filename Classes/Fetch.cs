using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Exiled.API.Features;
using InventorySystem.Items.Firearms.Modules;

namespace SaskycStylesEasy.Classes
{
    public static class Fetch
    {
        public static Regex TagRegex = new(@"(?<tag>\w+)(\s*\((?<args>[^\)]*)\))?\s*\{(?<body>[^}]*)\}", RegexOptions.Compiled);
        public static Regex PropertyRegex = new(@"(?<key>\w+)\s*:\s*(?<value>[^;]+);", RegexOptions.Compiled);

        public static void FetchAllPropertiesToTags()
        {
            Log.Debug("Starting fetch debug. MethodName: [FetchAllPropertiesToTags]");

            var dllDirectory = Paths.Plugins;
            if (dllDirectory == null) return;

            var sseFolderPath = Path.Combine(dllDirectory, "SaskycStylesEasy");
            if (!Directory.Exists(sseFolderPath))
            {
                Log.Error("Folder SaskycStylesEasy does not exist.");
                return;
            }

            var sseFiles = Directory.GetFiles(sseFolderPath, "*.sse", SearchOption.AllDirectories);

            // Clear out the existing tag list before repopulating
            Tag.List.Clear();

            foreach (var file in sseFiles)
                FetchFile(file);
        }

        public static void FetchFile(string file)
        {
            Log.Debug($"  FetchFile method executed. Arguments: \n    File: {Path.GetFileName(file)}");

            string content;
            try
            {
                content = File.ReadAllText(file);
            }
            catch (Exception ex)
            {
                Log.Error($"  File could not be read\n    {ex.Message}");
                return;
            }

            // Reverted regex: capture tagName, optional (args), and { body } up to the first }
            
            foreach (Match match in TagRegex.Matches(content))
            {
                var tagName = match.Groups["tag"].Value.Trim();
                var body = match.Groups["body"].Value.Trim();
                var argsGroup = match.Groups["args"].Success ? match.Groups["args"].Value : "";

                FetchTag(tagName, body, argsGroup);
            }
                
        }
        
        public static void FetchTag(string tagName, string body, string argsGroup)
        {
            Log.Debug($"    FetchTag method executed. Arguments:\n      tagName: {tagName}\n      body: {body}\n      argsGroup: {argsGroup}");
            var argList = argsGroup
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(arg => arg.Trim())
                .ToList();

            var fetchedTag = new Tag(
                tagName,
                new Dictionary<Property, string>(),
                argList
            );


            if (Tag.List.Any(existing =>
                existing.Name == fetchedTag.Name))
            {
                Log.Debug($"    Tag was duplicate. Skipping it.");
                return;
            }

            var propertyMatches = PropertyRegex.Matches(body);
            foreach (Match propMatch in propertyMatches)
            {
                var key = propMatch.Groups["key"].Value.Trim();
                var value = propMatch.Groups["value"].Value.Trim();
                FetchProperty(key, value, fetchedTag);
            }

            Tag.List.Add(fetchedTag);
        }

        public static void FetchProperty(string key, string value, Tag tag)
        {
            Log.Debug($"      FetchProperty method executed. Arguments:\n        key: {key}\n        value: {value}");
            // Check if this key corresponds to a registered Property subclass
            var registeredProp = Property.List
                .FirstOrDefault(p => p.Name.Equals(key, StringComparison.OrdinalIgnoreCase));

            if(registeredProp is null) //haven't found property, so I register it as an variable
            {
                tag.Variables[key.ToLower()] = value;
                return;
            }

            if (!tag.Properties.ContainsKey(registeredProp))
            {
                tag.Properties.Add(registeredProp, value);
                return;
            }

            tag.Properties[registeredProp] = value;
        }
    }
}
