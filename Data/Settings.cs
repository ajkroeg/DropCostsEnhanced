﻿using System;
using System.Collections.Generic;

namespace DropCostsEnhanced.Data
{
    public class Settings
    {
        public bool debug = false;
        public bool trace = false;
        public bool enableDropCosts = true;
        public bool enableAmmoCosts = true;
        public bool enableHeatCosts = false;
        public float costFactor = 0.002f;
        public bool useCostByTons = false;
        public float dropCostPerTon = 500f;
        public float roundToNearist = 10000f;
        public string heatSunkStat = "CACOverrallHeatSinked";
    }
}