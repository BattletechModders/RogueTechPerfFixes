using BattleTech;
using BattleTech.Data;
using Harmony;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleTech.SimGameState;

namespace RogueTechPerfFixes.HarmonyPatches
{

	public static class HeraldryHelper
	{
		public static void RequestResources(HeraldryDef heraldryDef, BattleTech.Data.DataManager dataManager, Action loadCompleteCallback = null)
		{
			LoadRequest loadRequest = dataManager.CreateLoadRequest(delegate
			{
				heraldryDef.Refresh();
				if (loadCompleteCallback != null)
				{
					loadCompleteCallback();
				}
			});
			loadRequest.AddBlindLoadRequest(BattleTechResourceType.Texture2D, heraldryDef.textureLogoID, false);
			loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, heraldryDef.textureLogoID, false);
			loadRequest.AddBlindLoadRequest(BattleTechResourceType.ColorSwatch, heraldryDef.primaryMechColorID, false);
			loadRequest.AddBlindLoadRequest(BattleTechResourceType.ColorSwatch, heraldryDef.secondaryMechColorID, false);
			loadRequest.AddBlindLoadRequest(BattleTechResourceType.ColorSwatch, heraldryDef.tertiaryMechColorID, false);
			loadRequest.ProcessRequests();
		}
	}

	[HarmonyPatch(typeof(SimGameState), "RequestDataManagerResources")]
	static class SimGameState_RequestDataManagerResources
	{
		static bool Prefix(SimGameState __instance, string ___CONVERSATION_TEXTURE_ADDENDUM, string ___PLAYER_CREST_ADDENDUM)
		{
			Action<LoadRequest> respondToDefsComplete = (LoadRequest lr) =>
			{
				logger.Log($"RespondToDefsLoadComplete DONE");
				// __instance.RespondToDefsLoadComplete
			};
			//LoadRequest loadRequest = __instance.DataManager.CreateLoadRequest(respondToDefsComplete, filterByOwnership: true);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.SimGameEventDef, true);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.StarSystemDef, false);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.ContractOverride, true);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.SimGameStringList, false);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.LifepathNodeDef, false);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.ShopDef, false);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.FactionDef, true);
			//loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.FlashpointDef, true);

			//string[] startingMechWarriors = __instance.Constants.Story.StartingMechWarriors;
			//foreach (string resourceId in startingMechWarriors)
			//{
			//	loadRequest.AddBlindLoadRequest(BattleTechResourceType.PilotDef, resourceId, false);
			//}
			//startingMechWarriors = __instance.Constants.Story.StartingMechWarriorPortraits;
			//foreach (string resourceId2 in startingMechWarriors)
			//{
			//	loadRequest.AddBlindLoadRequest(BattleTechResourceType.PortraitSettings, resourceId2, false);
			//}

			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.PilotDef, __instance.Constants.Story.DefaultCommanderID, false);

			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.MechDef, __instance.Constants.Story.StartingPlayerMech, false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.MechDef, __instance.Constants.Story.StartingPlayerMech, false);

			//// Sprites & SVGAssets
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrIcon_atlas", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrIcon_atlas", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrIcon_atlas", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrIcon_atlas", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrIcon_inOrbit", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrIcon_roomArgo", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrIcon_atlas", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SVGAsset, "uixSvgIcon_mwrank_Ronin", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SVGAsset, "uixSvgIcon_mwrank_KSBacker", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SVGAsset, "uixSvgIcon_mwrank_Commander", false);
			//for (int j = 0; j < 5; j++)
			//{
			//	loadRequest.AddBlindLoadRequest(BattleTechResourceType.SVGAsset, string.Format("{0}{1}{2}", "uixSvgIcon_mwrank_", "Rank", j + 1), false);
			//}
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SVGAsset, "uixSvgIcon_generic_MechPart", false);

			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SimGameDifficultySettingList, "DifficultySettings", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SimGameDifficultySettingList, "CareerDifficultySettings", false);
			//foreach (SimGameCrew value in Enum.GetValues(typeof(SimGameCrew)))
			//{
			//	string resourceId3 = string.Format("{0}{1}{2}", "castDef_", value.ToString().Substring("Crew_".Length), "Default");
			//	loadRequest.AddBlindLoadRequest(BattleTechResourceType.CastDef, resourceId3, false);
			//}
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.GenderedOptionsListDef, __instance.Constants.Pilot.PilotPortraits, false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.GenderedOptionsListDef, __instance.Constants.Pilot.PilotVoices, false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.GenderedOptionsListDef, __instance.Constants.Pilot.PilotPortraits, false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.GenderedOptionsListDef, __instance.Constants.Pilot.PilotVoices, false);
			//foreach (PilotDef_MDD roninPilotDef in MetadataDatabase.Instance.GetRoninPilotDefs(checkOwnership: true))
			//{
			//	loadRequest.AddBlindLoadRequest(BattleTechResourceType.PilotDef, roninPilotDef.PilotDefID, false);
			//}

			//VersionManifestAddendum addendumByName = __instance.DataManager.ResourceLocator.GetAddendumByName(___CONVERSATION_TEXTURE_ADDENDUM);
			//VersionManifestEntry[] array = __instance.DataManager.ResourceLocator.AllEntriesOfResourceFromAddendum(BattleTechResourceType.Texture2D, addendumByName);
			//foreach (VersionManifestEntry versionManifestEntry in array)
			//{
			//	loadRequest.AddBlindLoadRequest(BattleTechResourceType.Texture2D, versionManifestEntry.Id, false);
			//}
			//VersionManifestAddendum addendumByName2 = __instance.DataManager.ResourceLocator.GetAddendumByName(___PLAYER_CREST_ADDENDUM);
			//array = __instance.DataManager.ResourceLocator.AllEntriesOfResourceFromAddendum(BattleTechResourceType.Sprite, addendumByName2);
			//foreach (VersionManifestEntry versionManifestEntry2 in array)
			//{
			//	loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, versionManifestEntry2.Id, false);
			//}
			//__instance.Player1sMercUnitHeraldryDef = __instance.Constants.Player1sMercUnitHeraldryDef;
			//HeraldryHelper.RequestResources(__instance.Player1sMercUnitHeraldryDef, __instance.DataManager);

			//foreach (SimGameCharacterType value2 in Enum.GetValues(typeof(SimGameCharacterType)))
			//{
			//	if (value2 != 0)
			//	{
			//		string text = "TooltipSimGameCharacter" + value2;
			//		if (__instance.DataManager.ResourceLocator.EntryByID(text, BattleTechResourceType.BackgroundDef) != null)
			//		{
			//			loadRequest.AddBlindLoadRequest(BattleTechResourceType.BaseDescriptionDef, text, false);
			//		}
			//	}
			//}

			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SimpleText, "careerModeAllLightChassis", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SimpleText, "careerModeAllMediumChassis", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SimpleText, "careerModeAllHeavyChassis", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.SimpleText, "careerModeAllAssaultChassis", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrSpot_flashpointExample", false);
			//loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, "uixTxrSpot_StarmapV2-Example", false);
			//RTPFLogger.Debug?.Write($"PROCESSING INITIAL BATCH");
			//loadRequest.ProcessRequests();

			// Bulk loads
			//CreateAllLoadRequest(BattleTechResourceType.MechDef, __instance.DataManager, true);

			//CreateAllLoadRequest(BattleTechResourceType.HeraldryDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.SimGameConversations, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.CastDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.SimGameSpeakers, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.BackgroundDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.SimGameMilestoneDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.SimGameStatDescDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.AbilityDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.ShipModuleUpgrade, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.SimGameSubstitutionListDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.PortraitSettings, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.AudioEventDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.WeaponDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.AmmunitionDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.AmmunitionBoxDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.HeatSinkDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.UpgradeDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.BaseDescriptionDef, __instance.DataManager);

			//CreateAllLoadRequest(BattleTechResourceType.ItemCollectionDef, __instance.DataManager, true);

			logger.Log($"Returning FALSE from RequestDataManagerResources");
			return true;
		}
		
		private static void CreateAllLoadRequest(BattleTechResourceType type, BattleTech.Data.DataManager dataManager, bool filterByOwnership=false)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			Action<LoadRequest> onComplete = (LoadRequest lr) =>
			{
				logger.Log($"Load request complete for {type} in {sw.ElapsedMilliseconds / 1000f}s  sw_id:{sw.GetHashCode()}");
				sw.Stop();
			};

			VersionManifestEntry[] array = dataManager.ResourceLocator.AllEntriesOfResource(type);
			logger.Log($"Load request started for {type} with {array.Length} entries with sw_id:{sw.GetHashCode()}");
			LoadRequest loadRequest = dataManager.CreateLoadRequest(onComplete, filterByOwnership: true);
			loadRequest.AddAllOfTypeBlindLoadRequest(type, filterByOwnership);
			loadRequest.ProcessRequests();
		}
	}
}
