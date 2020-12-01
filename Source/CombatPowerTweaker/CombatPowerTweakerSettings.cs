using System;
using System.Collections.Generic;
using System.Linq;
using SettingsHelper;
using UnityEngine;
using Verse;

namespace CombatPowerTweaker
{
    class CombatPowerTweakerSettings : ModSettings
    {
        public Dictionary<string, float> vanillaMemory = new Dictionary<string, float>();
        public Dictionary<string, string> pawnKindNames = new Dictionary<string, string>();
        public Dictionary<string, float> modifiedStats = new Dictionary<string, float>();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref modifiedStats, "modifiedStats", LookMode.Value, LookMode.Value, ref modifiedStatsKeys, ref modifiedStatsValues);
        }

        private List<string> modifiedStatsKeys;
        private List<float> modifiedStatsValues;

        public void DoSettingsWindowContents(Rect inRect)
        {
            var keys = modifiedStats.Keys.ToList();
            keys.Reverse();
            var rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            var rect2 = new Rect(0f, 0f, inRect.width - 30f, keys.Count * 25);
            Widgets.BeginScrollView(rect, ref scrollPosition, rect2, true);
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(rect2);
            listingStandard.Label("Combat power, vanilla value in parenthesis");
            if (listingStandard.ButtonTextLabeled("Reset all to vanilla", "Reset"))
            {
                foreach (var keyValue in CombatPowerTweakerMod.settings.vanillaMemory)
                {
                    CombatPowerTweakerMod.settings.modifiedStats[keyValue.Key] = keyValue.Value;
                }
            }
            if (listingStandard.ButtonTextLabeled("Increase all values by 10%", "+10%"))
            {
                foreach (var keyValue in CombatPowerTweakerMod.settings.vanillaMemory)
                {
                    CombatPowerTweakerMod.settings.modifiedStats[keyValue.Key] = CombatPowerTweakerMod.settings.modifiedStats[keyValue.Key] * 1.1f;
                    if (CombatPowerTweakerMod.settings.modifiedStats[keyValue.Key] > maxValue)
                    {
                        CombatPowerTweakerMod.settings.modifiedStats[keyValue.Key] = maxValue;
                    }
                }
            }
            if (listingStandard.ButtonTextLabeled("Decrease all values by 10%", "-10%"))
            {
                foreach (var keyValue in CombatPowerTweakerMod.settings.vanillaMemory)
                {
                    CombatPowerTweakerMod.settings.modifiedStats[keyValue.Key] = CombatPowerTweakerMod.settings.modifiedStats[keyValue.Key] * 0.9f;
                }
            }
            for (var num = keys.Count - 1; num >= 0; num--)
            {
                var test = modifiedStats[keys[num]];
                listingStandard.AddLabeledSlider($"{GenText.CapitalizeFirst(pawnKindNames[keys[num]])} ({vanillaMemory[keys[num]]})", ref test, 1f, maxValue, test.ToString(), null, 1);
                modifiedStats[keys[num]] = test;
            }
            listingStandard.End();
            Widgets.EndScrollView();
            Write();
        }

        private static Vector2 scrollPosition = Vector2.zero;

        private static readonly float maxValue = 2000f;
    }
}
