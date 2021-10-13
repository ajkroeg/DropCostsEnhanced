﻿using System;
using System.Collections.Generic;
using BattleTech;
using Harmony;
using DropCostsEnhanced;
using UnityEngine;

namespace DropCostsEnhanced.Patches
{
    [HarmonyPatch(typeof(Contract), "CompleteContract")]
    public static class Contract_CompleteContract_Patch {

        static void Postfix(Contract __instance)
        {
            try
            {
                CombatGameState combat = __instance.BattleTechGame.Combat;
                DCECore.modLog.Info?.Write($"Calculating Drop Cost for {__instance.Name}. Original MoneyResults: {__instance.MoneyResults}");
                List<AbstractActor> actors = combat.AllActors;


                int TotalCost = 0;
                if (DCECore.settings.enableDropCosts)
                {
                    TotalCost += DropCostManager.Instance.Cost;
                }

                if (DCECore.settings.enableAmmoCosts)
                {
                    TotalCost += AmmoCostManager.Instance.CalculateFinalCosts(actors);
                }
                if (DCECore.settings.enableHeatCosts)
                {
                    TotalCost += HeatCostManager.Instance.CalculateFinalCosts(actors);
                }
                DCECore.modLog.Info?.Write($"Total Drop Cost: {TotalCost}");

                int newResult = Mathf.FloorToInt(__instance.MoneyResults - TotalCost);
                Traverse.Create(__instance).Property("MoneyResults").SetValue(newResult);



            }
            catch (Exception e)
            {
                DCECore.modLog.Error?.Write(e);
            }
        }
    }
}
