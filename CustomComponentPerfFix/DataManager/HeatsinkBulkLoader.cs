using BattleTech;
using Harmony;
using HBS.Data;
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
            IEnumerable<VersionManifestEntry> ownedEntries = allEntries.Where(vme => dataManager.ContentPackIndex.IsResourceOwned(vme.Id))
                .Where(vme => !dataManager.HeatSinkDefs.Keys.Contains(vme.Id))
                .ToList();
            InitialLoad(allEntries);
        }


        private void InitialLoad(IEnumerable<VersionManifestEntry> allEntries)
        {

            Traverse heatSinkDefsT = Traverse.Create(this.dataManager).Field("heatSinkDefs");
            DictionaryStore<HeatSinkDef> heatSinkDefs = heatSinkDefsT.GetValue<DictionaryStore<HeatSinkDef>>();

            Dictionary<VersionManifestEntry, string> loadedContent = new Dictionary<VersionManifestEntry, string>();
            // Iterate through the files and load their content
            //Parallel.ForEach(allEntries, vme => {
            foreach (VersionManifestEntry vme in allEntries) 
            { 

                if (vme.IsFileAsset)
                {
                    //logger.Log($" Loading path '{vme.FilePath.Trim()}'");
                    using (FileStream arg = new FileStream(vme.FilePath, FileMode.Open, FileAccess.Read))
                    {
                        // Read type
                        StreamReader sr = new StreamReader(arg);
                        string content = sr.ReadToEnd();
                        loadedContent[vme] = content;

                        // Parse to new type
                        try
                        {
                            HeatSinkDef newDef = new HeatSinkDef();
                            newDef.FromJSON(content);

                            // Add to DM
                            heatSinkDefs.Add(vme.Id, newDef);
                            //logger.Log($" -- added resourceId: {vme.Id} at path {vme.FilePath.Trim()}");
                        }
                        catch (Exception e)
                        {
                            //logger.Log($" -- failed to load resourceId: {vme.Id} at path {vme.FilePath.Trim()}", e);
                        }

                    }
                }
                else
                {
                    logger.Log($" ERROR: DIDNT CODE A WAY TO LOAD FROM RESOURCES OR ASSET BUNDLES{vme}");
                }
            }

            logger.Log($"Heatsinks bulk loading complete. DataManager claims it has: {dataManager.HeatSinkDefs.Keys.Count()} keys.");

        }

    }
}
