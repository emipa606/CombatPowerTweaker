using System.Collections.Generic;
using System.Linq;
using SettingsHelper;
using UnityEngine;
using Verse;

namespace CombatPowerTweaker
{
    internal class CombatPowerTweakerSettings : ModSettings
    {
        private static readonly float maxValue = 2000f;

        private static Vector2 scrollPosition = Vector2.zero;

        public Dictionary<string, float> modifiedStats = new Dictionary<string, float>();

        public Dictionary<string, string> pawnKindNames = new Dictionary<string, string>();

        public Dictionary<string, float> vanillaMemory = new Dictionary<string, float>();

        private List<string> modifiedStatsKeys;

        private List<float> modifiedStatsValues;

        public void DoSettingsWindowContents(Rect inRect)
        {
            var keys = modifiedStats.Keys.ToList();
            keys.Reverse();
            var rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            var rect2 = new Rect(0f, 0f, inRect.width - 30f, keys.Count * 36);
            Widgets.BeginScrollView(rect, ref scrollPosition, rect2);
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(rect2);
            listingStandard.Label("Combat power, vanilla value in parenthesis");
            if (listingStandard.ButtonTextLabeled("Reset all to vanilla", "Reset"))
            {
                foreach (var keyValue in CombatPowerTweakerMod.Settings.vanillaMemory)
                {
                    CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] = keyValue.Value;
                }
            }

            if (listingStandard.ButtonTextLabeled("Increase all values by 10%", "+10%"))
            {
                foreach (var keyValue in CombatPowerTweakerMod.Settings.vanillaMemory)
                {
                    CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] = CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] * 1.1f;
                    if (CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] > maxValue)
                    {
                        CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] = maxValue;
                    }
                }
            }

            if (listingStandard.ButtonTextLabeled("Decrease all values by 10%", "-10%"))
            {
                foreach (var keyValue in CombatPowerTweakerMod.Settings.vanillaMemory)
                {
                    CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] = CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] * 0.9f;
                }
            }

            for (var num = keys.Count - 1; num >= 0; num--)
            {
                var test = modifiedStats[keys[num]];
                listingStandard.AddLabeledSlider($"{pawnKindNames[keys[num]].CapitalizeFirst()} ({vanillaMemory[keys[num]]})", ref test, 1f, maxValue, test.ToString(), null, 1);
                modifiedStats[keys[num]] = test;
            }

            listingStandard.End();
            Widgets.EndScrollView();
            Write();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref modifiedStats, "modifiedStats", LookMode.Value, LookMode.Value, ref modifiedStatsKeys, ref modifiedStatsValues);
        }
    }
}