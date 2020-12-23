using BattleTech;
using BattleTech.Data;
using Harmony;
using RogueTechPerfFixes.DataManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleTech.Data.DataManager;
using static BattleTech.SimGameState;

namespace RogueTechPerfFixes.HarmonyPatches
{
	[HarmonyPatch(typeof(BattleTech.Data.DataManager), "ProcessPrewarmRequests")]
	static class DataManager_ProcessPrewarmRequests
	{
		static void Prefix(BattleTech.Data.DataManager __instance, IEnumerable<PrewarmRequest> toPrewarm)
		{
			logger.Log($"Processing preWarm dependencies:");
			foreach (PrewarmRequest req in toPrewarm)
            {
				if (req.PrewarmAllOfType)
					logger.Log($" -- t: {req.ResourceType}  allOfType: {req.PrewarmAllOfType}");
				else
					logger.Log($" -- t: {req.ResourceType}  rId: {req.ResourceID}");
			}

			// TODO: Preload via our tooling instead
			HeatsinkBulkLoader hbl = new HeatsinkBulkLoader(__instance);
			Stopwatch sw = new Stopwatch();
			sw.Start();
			logger.Log($"Starting bulk load of heatsinks");
			hbl.LoadAll();
			sw.Stop();
			logger.Log($"Bulk load of heatsinks complete in {sw.ElapsedMilliseconds}ms");
		}
	}

	//[HarmonyPatch(typeof(BattleTech.Data.DataManager), "CreateLoadRequest")]
	//static class DataManager_CreateLoadRequest
	//{
	//	static void Postfix(BattleTech.Data.DataManager __instance, LoadRequest __result)
	//	{
	//		StackTrace st = new StackTrace();
	//		string stS = st.ToString();
	//		bool isDepLoad = stS.Contains("BattleTech.Data.DataManager.RequestDependencies");
	//		if (__result != null)
 //           {
	//			if (isDepLoad)
	//				logger.Log($"DM new dependency load with hashCode: {__result.GetHashCode()}");
	//			else
	//				logger.Log($"DM novel CreateLoadRequest with hashCode: {__result.GetHashCode()} invoked from: {st}");
	//		}
				
	//	}
	//}

	[HarmonyPatch(typeof(DependencyLoadRequest), "RequestResource")]
	static class DependencyLoadRequest_RequestResource
	{
		static void Prefix(DependencyLoadRequest __instance, BattleTechResourceType type, string id)
		{
			logger.Log($"Dependency load for type: {type}  with id: {id}");
		}
	}

	[HarmonyPatch(typeof(DependencyLoadRequest), "RequestAllResourcesOfType")]
	static class DependencyLoadRequest_RequestAllResourcesOfType
	{ 
		static void Prefix(DependencyLoadRequest __instance, BattleTechResourceType type)
        {
			logger.Log($"Dependency load for all of type: {type}");
        }
	}
}
