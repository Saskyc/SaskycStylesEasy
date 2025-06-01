using System;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using SaskycStylesEasy.Classes;
using SaskycStylesTestt.Classes;

namespace SaskycStylesTestt 
{
    public class SaskycStylesEasy : Plugin<Config>
    {
        public static SaskycStylesEasy Instance;
        
        public override void OnEnabled()
        {
            Instance = this;
            
            Log.Debug("Start of program.");
            Ensuring.EnsureSaskycStylesFolder();
            Ensuring.PrintAllSseFiles();
            
            Property.RegisterAll();
            
            Fetch.FetchAllPropertiesToTags();
            
            Log.Debug("----------All of tags registered:");
            foreach (var property in Property.List)
            {
                Log.Debug(property.Name);
                
                //if (property.End == string.Empty)
                //{
                //    Log.Debug($"{property.Start.Replace("%value%", property.ParserValue.ToString())}ExampleText");
                //    continue;
                //}
                //
                //Log.Debug($"{property.Start.Replace("%value%", property.ParserValue.ToString())}ExampleText{property.End}");
            }

            Log.Debug("----------All of FOUND tags registered:");
            foreach (var tag in Tag.List)
            {
                Log.Debug($"Name: {tag.Name}");
                Log.Debug("Properties:");
                foreach (var property in tag.Properties)
                {
                    Log.Debug($"  {property.Key.Name}: {property.Value};");
                }
            }
            
            //Tag.ExecuteTag("myTag", ["#whatever"], "text");
            
            //var text = (Tag.ExecuteTag("MyColorTag", ["#whatever"], "text"));
            Log.Debug("'''''''''''''''''''");
            //Log.Debug(text);
            Log.Debug("'''''''''''''''''''");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            
            base.OnDisabled();
        }

        public override string Name { get; } = "SaskycStylesEasy";
        public override string Author { get; } = "Saskyc & ChatGPT";
    }
}