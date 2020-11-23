using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace CombatPowerTweaker
{
    class CombatPowerTweakerMod : Mod
    {
        public static CombatPowerTweakerSettings settings;
        public CombatPowerTweakerMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<CombatPowerTweakerSettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Combat Power Tweaker";
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
            CombatPowerTweaker.SetCombatPowerValues();
        }
    }

    [StaticConstructorOnStartup]
    public static class SettingsInit
    {
        static SettingsInit()
        {
            var thingsWithCombatPower = from PawnKindDef pawnKindDef in DefDatabase<PawnKindDef>.AllDefsListForReading orderby pawnKindDef.label select pawnKindDef;

            if (CombatPowerTweakerMod.settings.modifiedStats == null)
            {
                CombatPowerTweakerMod.settings.modifiedStats = new Dictionary<string, float>();
            }
            CombatPowerTweakerMod.settings.vanillaMemory = new Dictionary<string, float>();
            CombatPowerTweakerMod.settings.pawnKindNames = new Dictionary<string, string>();
            foreach (var pawnKind in thingsWithCombatPower)
            {
                CombatPowerTweakerMod.settings.vanillaMemory[pawnKind.defName] = pawnKind.combatPower;
                CombatPowerTweakerMod.settings.pawnKindNames[pawnKind.defName] = pawnKind.label;
                if (CombatPowerTweakerMod.settings.modifiedStats.ContainsKey(pawnKind.defName))
                {
                    continue;
                }
                CombatPowerTweakerMod.settings.modifiedStats[pawnKind.defName] = pawnKind.combatPower;
            }
        }
    }
}
