using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace CombatPowerTweaker;

internal class CombatPowerTweakerSettings : ModSettings
{
    private const float MaxValue = 2000f;

    private static Vector2 scrollPosition = Vector2.zero;

    private static readonly Vector2 searchSize = new(200f, 25f);

    public Dictionary<string, float> ModifiedStats = new();

    private List<string> modifiedStatsKeys;

    private List<float> modifiedStatsValues;

    public Dictionary<string, string> PawnKindNames = new();

    private string searchText = "";

    public Dictionary<string, float> VanillaMemory = new();

    public void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.Label("CPT.combatpower.label".Translate());
        if (listingStandard.ButtonTextLabeled("CPT.reset.label".Translate(), "CPT.reset.button".Translate()))
        {
            foreach (var keyValue in CombatPowerTweakerMod.Settings.VanillaMemory)
            {
                CombatPowerTweakerMod.Settings.ModifiedStats[keyValue.Key] = keyValue.Value;
            }
        }

        if (CombatPowerTweakerMod.CurrentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("CPT.modversion.label".Translate(CombatPowerTweakerMod.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        if (listingStandard.ButtonTextLabeled("CPT.percentup.label".Translate(), "CPT.percentup.button".Translate()))
        {
            foreach (var keyValue in CombatPowerTweakerMod.Settings.VanillaMemory)
            {
                CombatPowerTweakerMod.Settings.ModifiedStats[keyValue.Key] *= 1.1f;
                if (CombatPowerTweakerMod.Settings.ModifiedStats[keyValue.Key] > MaxValue)
                {
                    CombatPowerTweakerMod.Settings.ModifiedStats[keyValue.Key] = MaxValue;
                }
            }
        }

        if (listingStandard.ButtonTextLabeled("CPT.percentdown.label".Translate(),
                "CPT.percentdown.button".Translate()))
        {
            foreach (var keyValue in CombatPowerTweakerMod.Settings.VanillaMemory)
            {
                CombatPowerTweakerMod.Settings.ModifiedStats[keyValue.Key] *= 0.9f;
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
        var keys = ModifiedStats.Keys.ToList();
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
            ModifiedStats[keys[num]] = listingScroll.SliderLabeled(
                $"{PawnKindNames[keys[num]].CapitalizeFirst()} ({VanillaMemory[keys[num]]}): {Math.Round(ModifiedStats[keys[num]])}",
                ModifiedStats[keys[num]],
                1f,
                MaxValue, 0.5f, ModifiedStats[keys[num]].ToString());
        }

        listingScroll.End();
        Widgets.EndScrollView();
        Write();
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref ModifiedStats, "modifiedStats", LookMode.Value, LookMode.Value,
            ref modifiedStatsKeys, ref modifiedStatsValues);
    }
}