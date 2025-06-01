using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Exiled.API.Features;

namespace SaskycStylesTestt.Classes
{
    public class Ensuring
    {
        public static void EnsureSaskycStylesFolder()
        {
            // Get directory where the current DLL is located
            var dllDirectory = Paths.Plugins;
            
            if (dllDirectory == null)
            {
                Log.Error("dllDirectory is null, something went wrong");
                return;
            }
            
            Log.Debug($"Created it here: {dllDirectory}");
            
            // Target folder path
            var sseFolderPath = Path.Combine(dllDirectory, "SaskycStylesEasy");

            // Create folder if it doesn't exist
            if (!Directory.Exists(sseFolderPath))
            {
                Directory.CreateDirectory(sseFolderPath);
                Log.Debug("✅ Created folder: SaskycStylesEasy");
            }

            // Create an example file if it doesn't exist
            var exampleFilePath = Path.Combine(sseFolderPath, "example.sse");

            if (File.Exists(exampleFilePath)) return;
            
            const string exampleContent = "planetTag(float pos, float rot) {\n  text: 🌍;\n  rotate: rot;\n  pos: pos;\n  size: 50;\n  alpha: #50;\n  color: #131313;\n}\nplanetLine() {\n  Repeat: planetTag(i * 50, i * 50), 5;\n}";
            
            File.WriteAllText(exampleFilePath, exampleContent);
            Log.Debug("✅ Created example.sse with sample content");
        }
        
        public static void PrintAllSseFiles()
        {
            var dllDirectory = Paths.Plugins;

            if (dllDirectory == null) return;

            var sseFolderPath = Path.Combine(dllDirectory, "SaskycStylesEasy");

            if (!Directory.Exists(sseFolderPath))
            {
                Log.Error("❌ Folder SaskycStylesEasy does not exist.");
                return;
            }

            // Search recursively for all .sse files
            var sseFiles = Directory.GetFiles(sseFolderPath, "*.sse", SearchOption.AllDirectories);

            foreach (var file in sseFiles)
            {
                Log.Debug($"\n📄 Reading file: {Path.GetFileName(file)} (Path: {file.Replace(dllDirectory + Path.DirectorySeparatorChar, "")})");

                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    Log.Debug(line);
                }
            }
        }
    }
}