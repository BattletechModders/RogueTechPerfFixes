using BattleTech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleTech.SimGameState;

namespace RogueTechPerfFixes.DataManager
{
    public class HeatsinkBulkLoader
    {
        private BattleTech.Data.DataManager dataManager;

        public HeatsinkBulkLoader(BattleTech.Data.DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public void LoadAll()
        {
            VersionManifestEntry[] allEntries = dataManager.ResourceLocator.AllEntriesOfResource(BattleTechResourceType.HeatSinkDef);
            IEnumerable<VersionManifestEntry> ownedEntries = FilterByOwnership(allEntries);
            InitialLoad(allEntries);
        }

        private IEnumerable<VersionManifestEntry> FilterByOwnership(IEnumerable<VersionManifestEntry> allEntries)
        {
            return allEntries.Where(vme => dataManager.ContentPackIndex.IsResourceOwned(vme.Id)).ToList();
        }

        private void InitialLoad(IEnumerable<VersionManifestEntry> allEntries)
        {

            Dictionary<VersionManifestEntry, string> loadedContent = new Dictionary<VersionManifestEntry, string>();
            // Iterate through the files and load their content
            Parallel.ForEach(allEntries, vme => {

                if (vme.IsFileAsset)
                {
                    logger.Log($" Loading path {vme.FilePath.Trim()}");
                    using (FileStream arg = new FileStream(vme.FilePath, FileMode.Open, FileAccess.Read))
                    {
                        StreamReader sr = new StreamReader(arg);
                        string content = sr.ReadToEnd();
                        loadedContent[vme] = content;

                    }
                }
                else
                {
                    logger.Log($" ERROR: DIDNT CODE A WAY TO LOAD FROM ASSET BUNDLES{vme}");
                }

                

            });
        }

    }
}
