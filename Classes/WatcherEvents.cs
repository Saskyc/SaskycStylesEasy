using Exiled.API.Features;
using Exiled.CreditTags.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaskycStylesEasy.Classes
{
    public class WatcherEvents
    {
        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Fetch.FetchFile(e.FullPath);

            var tags = Tag.List.FindAll(x => x.Path == e.FullPath);
            foreach (var tag in tags)
                Tag.List.Remove(tag);

            Log.Debug($"Changed: {e.FullPath}");
        }

        public static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Log.Debug(value);

            Fetch.FetchFile(value);
        }

        public static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Log.Debug($"Deleted: {e.FullPath}");
            var tags = Tag.List.FindAll(x => x.Path == e.FullPath);
            foreach(var tag in tags)
                Tag.List.Remove(tag);
        }

        public static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Log.Debug($"Renamed:");
            Log.Debug($"    Old: {e.OldFullPath}");
            Log.Debug($"    New: {e.FullPath}");
            var tags = Tag.List.FindAll(x => x.Path == e.FullPath);
            foreach(var tag in tags)
            {
                tag.Path = e.FullPath;
            }
        }
    }
}
