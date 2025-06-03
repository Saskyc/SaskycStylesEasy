using Exiled.API.Features;
using SaskycStylesEasy.Classes;
using System.IO;

namespace SaskycStylesEasy 
{
    public class SaskycStylesEasy : Plugin<Config>
    {
        public static SaskycStylesEasy Instance;
        
        public override void OnEnabled()
        {
            Instance = this;
            Property.RegisterAll();
            Ensuring.EnsureSaskycStylesFolder();
            Fetch.FetchAllPropertiesToTags();

            var sseFolderPath = Path.Combine(Paths.Plugins, "SaskycStylesEasy");

            using var watcher = new FileSystemWatcher(sseFolderPath);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += WatcherEvents.OnChanged;
            watcher.Created += WatcherEvents.OnCreated;
            watcher.Deleted += WatcherEvents.OnDeleted;
            watcher.Renamed += WatcherEvents.OnRenamed;

            watcher.Filter = "*.sse";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

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