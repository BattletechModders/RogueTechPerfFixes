using BattleTech;
using BattleTech.Data;
using Harmony;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static BattleTech.SimGameState;

namespace RogueTechPerfFixes.HarmonyPatches
{

	[HarmonyPatch]
	static class HeatSinkDefLoadRequest_StoreData
    {
		static MethodBase TargetMethod()
		{
			return AccessTools.Method("HeatSinkDefLoadRequest:StoreData");
		}

		static void Prefix(string ___resourceId)
        {
			logger.Log($"Adding HeatSinkDef to data manager for resource: {___resourceId}");
		}

    }


	[HarmonyPatch(typeof(LoadRequest), "TryCreateAndAddLoadRequest")]
	static class LoadRequest_TryCreateAndAddLoadRequest
	{
		static void Prefix(LoadRequest __instance, BattleTechResourceType resourceType, string resourceId, bool? filterByOwnership, 
			BattleTech.Data.DataManager ___dataManager, bool ___filterByOwnershipDefault)
		{

			if (___dataManager == null)
				logger.Log($"WARNING - Datamanager is null!");

			VersionManifestEntry versionManifestEntry = ___dataManager.ResourceLocator.EntryByID(resourceId, resourceType);
			if (versionManifestEntry == null)
            {
				logger.Log($"WARNING - could not find versionManifestEntry for resourceId: {resourceId} with type: {resourceType}");
				return;
			}

			bool ownershipDefault = ___filterByOwnershipDefault;
			if (filterByOwnership.HasValue)
				ownershipDefault = filterByOwnership.Value;
			bool isResourceOwned = ___dataManager.ContentPackIndex.IsResourceOwned(versionManifestEntry.Id);
			bool flag = !ownershipDefault || isResourceOwned;

			if (resourceType == BattleTechResourceType.HeatSinkDef || resourceType == BattleTechResourceType.MechDef)
			{
				bool resourceExists = ___dataManager.HeatSinkDefs.Keys.Contains(resourceId);
				logger.Log($"TCAALR for t: {resourceType} and rId: {resourceId}  " +
					$"ownershipDefault: {___filterByOwnershipDefault}  param: {filterByOwnership}  isResourceOwned: {isResourceOwned}  flag: {flag}  dmHasResource: {resourceExists}" +
					$"  created for loadRequest: {__instance.GetHashCode()}");
			}

		}
	
	}
}
