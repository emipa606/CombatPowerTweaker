using System.Collections.Generic;
using System.Linq;
using Verse;

namespace CombatPowerTweaker;

[StaticConstructorOnStartup]
internal static class CombatPowerTweaker
{
    static CombatPowerTweaker()
    {
        settingsInit();
        SetCombatPowerValues();
    }

    public static void SetCombatPowerValues()
    {
        var changedDefs = 0;

        foreach (var pawnKind in DefDatabase<PawnKindDef>.AllDefsListForReading)
        {
            changedDefs++;
            if (!CombatPowerTweakerMod.Settings.ModifiedStats.ContainsKey(pawnKind.defName) ||
                CombatPowerTweakerMod.Settings.ModifiedStats[pawnKind.defName] ==
                CombatPowerTweakerMod.Settings.VanillaMemory[pawnKind.defName])
            {
                continue;
            }

            pawnKind.combatPower = CombatPowerTweakerMod.Settings.ModifiedStats[pawnKind.defName];
        }

        if (changedDefs > 0)
        {
            Log.Message($"CombatPowerTweaker: Changed the combat power for {changedDefs} PawnKinds");
        }
    }

    private static void settingsInit()
    {
        var thingsWithCombatPower = from PawnKindDef pawnKindDef in DefDatabase<PawnKindDef>.AllDefsListForReading
            orderby pawnKindDef.label
            select pawnKindDef;

        if (CombatPowerTweakerMod.Settings.ModifiedStats == null)
        {
            CombatPowerTweakerMod.Settings.ModifiedStats = new Dictionary<string, float>();
        }
        else
        {
            CombatPowerTweakerMod.Settings.ModifiedStats.RemoveAll(pair =>
                DefDatabase<PawnKindDef>.GetNamedSilentFail(pair.Key) == null);
        }

        CombatPowerTweakerMod.Settings.VanillaMemory = new Dictionary<string, float>();
        CombatPowerTweakerMod.Settings.PawnKindNames = new Dictionary<string, string>();
        foreach (var pawnKind in thingsWithCombatPower)
        {
            CombatPowerTweakerMod.Settings.VanillaMemory[pawnKind.defName] = pawnKind.combatPower;
            CombatPowerTweakerMod.Settings.PawnKindNames[pawnKind.defName] = pawnKind.label;
            if (CombatPowerTweakerMod.Settings.ModifiedStats.ContainsKey(pawnKind.defName))
            {
                continue;
            }

            CombatPowerTweakerMod.Settings.ModifiedStats[pawnKind.defName] = pawnKind.combatPower;
        }
    }
}