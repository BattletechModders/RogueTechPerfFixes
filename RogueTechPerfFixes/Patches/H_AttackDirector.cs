﻿using BattleTech;
using RogueTechPerfFixes.Models;
using RogueTechPerfFixes.Utils;

namespace RogueTechPerfFixes.Patches;

public static class H_AttackDirector
{
    private static int _counter = 0;

    /// <summary>
    /// Along with patch to <see cref="AttackDirector.OnAttackSequenceEnd"/>, they together pool all work
    /// of refreshing the visibility cache into one place.
    /// </summary>
    [HarmonyPatch(typeof(AttackDirector), nameof(AttackDirector.OnAttackSequenceBegin))]
    public static class H_OnAttackSequenceBegin
    {
        public static bool Prepare()
        {
            return Mod.Settings.Patch.LowVisibility;
        }

        public static void Postfix()
        {
            VisibilityCacheGate.EnterGate();
            _counter = VisibilityCacheGate.GetCounter;
            RTPFLogger.Debug?.Write($"Enter visibility cache gate in {typeof(H_OnAttackSequenceBegin).FullName}:{nameof(Postfix)}\n");
        }
    }

    [HarmonyPatch(typeof(AttackDirector), nameof(AttackDirector.OnAttackSequenceEnd))]
    public static class H_OnAttackSequenceEnd
    {
        public static bool Prepare()
        {
            return Mod.Settings.Patch.LowVisibility;
        }

        public static void Postfix()
        {
            VisibilityCacheGate.ExitGate();

            Utils.Utils.CheckExitCounter($"Fewer calls made to ExitGate() when reaches AttackDirector.OnAttackSequenceEnd().\n", _counter);
            RTPFLogger.Debug?.Write($"Exit visibility cache gate in {typeof(H_OnAttackSequenceEnd).FullName}:{nameof(Postfix)}\n");
        }
    }
}