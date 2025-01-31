using System;
using System.Collections.Generic;
using BattleTech;
using BattleTech.Framework;
using Harmony;
using DropCostsEnhanced;
using DropCostsEnhanced.Data;
using UnityEngine;

namespace DropCostsEnhanced.Patches
{
    [HarmonyPatch(typeof(ContractOverride), "GetUIDifficulty")]
    public static class ContractOverride_GetUIDifficulty_Patch {
        
        public static bool Prepare()
        {
            return DCECore.settings.diffMode != EDifficultyType.NotActive && DCECore.settings.useDiffRungs;
        }
        
        static void Postfix(ContractOverride __instance, ref int __result)
        {
            try
            {
                __result = Math.Max(__instance.finalDifficulty + __instance.difficultyUIModifier, 1);
            }
            catch (Exception e)
            {
                DCECore.modLog.Error?.Write(e);
            }
        }
    }
}