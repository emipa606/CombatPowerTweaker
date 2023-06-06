using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace CombatPowerTweaker;

internal class CombatPowerTweakerSettings : ModSettings
{
    private static readonly float maxValue = 2000f;

    private static Vector2 scrollPosition = Vector2.zero;

    private static readonly Vector2 searchSize = new Vector2(200f, 25f);

    public Dictionary<string, float> modifiedStats = new Dictionary<string, float>();

    private List<string> modifiedStatsKeys;

    private List<float> modifiedStatsValues;

    public Dictionary<string, string> pawnKindNames = new Dictionary<string, string>();

    private string searchText = "";

    public Dictionary<string, float> vanillaMemory = new Dictionary<string, float>();

    public void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.Label("CPT.combatpower.label".Translate());
        if (listingStandard.ButtonTextLabeled("CPT.reset.label".Translate(), "CPT.reset.button".Translate()))
        {
            foreach (var keyValue in CombatPowerTweakerMod.Settings.vanillaMemory)
            {
                CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] = keyValue.Value;
            }
        }

        if (CombatPowerTweakerMod.currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("CPT.modversion.label".Translate(CombatPowerTweakerMod.currentVersion));
            GUI.contentColor = Color.white;
        }

        if (listingStandard.ButtonTextLabeled("CPT.percentup.label".Translate(), "CPT.percentup.button".Translate()))
        {
            foreach (var keyValue in CombatPowerTweakerMod.Settings.vanillaMemory)
            {
                CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] *= 1.1f;
                if (CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] > maxValue)
                {
                    CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] = maxValue;
                }
            }
        }

        if (listingStandard.ButtonTextLabeled("CPT.percentdown.label".Translate(),
                "CPT.percentdown.button".Translate()))
        {
            foreach (var keyValue in CombatPowerTweakerMod.Settings.vanillaMemory)
            {
                CombatPowerTweakerMod.Settings.modifiedStats[keyValue.Key] *= 0.9f;
            }
        }

        var searchLabel = listingStandard.Label("CPT.search.label".Translate());
        searchText =
            Widgets.TextField(
                new Rect(searchLabel.position + new Vector2((inRect.width / 3 * 2) - (searchSize.x / 3 * 2), 0),
                    searchSize),
                searchText);
        TooltipHandler.TipRegion(new Rect(
            searchLabel.position + new Vector2((inRect.width / 2) - (searchSize.x / 2), 0),
            searchSize), "CPT.search.label".Translate());
        var keys = modifiedStats.Keys.ToList();
        if (!string.IsNullOrEmpty(searchText))
        {
            keys = keys.Where(s => s.ToLower().Contains(searchText.ToLower())).ToList();
        }

        listingStandard.GapLine();

        listingStandard.End();

        keys.Reverse();
        var rect = new Rect(inRect.x, searchLabel.position.y + 80f, inRect.width,
            inRect.height - (searchLabel.position.y + 80f));
        var rect2 = new Rect(0f, 0f, inRect.width - 30f, keys.Count * 35);
        Widgets.BeginScrollView(rect, ref scrollPosition, rect2);
        var listingScroll = new Listing_Standard();
        listingScroll.Begin(rect2);
        for (var num = keys.Count - 1; num >= 0; num--)
        {
            modifiedStats[keys[num]] = listingScroll.SliderLabeled(
                $"{pawnKindNames[keys[num]].CapitalizeFirst()} ({vanillaMemory[keys[num]]}): {Math.Round(modifiedStats[keys[num]])}",
                modifiedStats[keys[num]],
                1f,
                maxValue, 0.5f, modifiedStats[keys[num]].ToString());
        }

        listingScroll.End();
        Widgets.EndScrollView();
        Write();
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref modifiedStats, "modifiedStats", LookMode.Value, LookMode.Value,
            ref modifiedStatsKeys, ref modifiedStatsValues);
    }
}