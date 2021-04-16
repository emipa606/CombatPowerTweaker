using System.Collections.Generic;
using System.Linq;
using Verse;

namespace CombatPowerTweaker
{
    [StaticConstructorOnStartup]
    internal static class CombatPowerTweaker
    {
        static CombatPowerTweaker()
        {
            SettingsInit();
            SetCombatPowerValues();
        }

        public static void SetCombatPowerValues()
        {
            var changedDefs = 0;

            foreach (var pawnKind in DefDatabase<PawnKindDef>.AllDefsListForReading)
            {
                changedDefs++;
                if (!CombatPowerTweakerMod.Settings.modifiedStats.ContainsKey(pawnKind.defName) || CombatPowerTweakerMod.Settings.modifiedStats[pawnKind.defName] == CombatPowerTweakerMod.Settings.vanillaMemory[pawnKind.defName])
                {
                    continue;
                }

                pawnKind.combatPower = CombatPowerTweakerMod.Settings.modifiedStats[pawnKind.defName];
            }

            if (changedDefs > 0)
            {
                Log.Message($"CombatPowerTweaker: Changed the combat power for {changedDefs} PawnKinds");
            }
        }

        private static void SettingsInit()
        {
            var thingsWithCombatPower = from PawnKindDef pawnKindDef in DefDatabase<PawnKindDef>.AllDefsListForReading orderby pawnKindDef.label select pawnKindDef;

            if (CombatPowerTweakerMod.Settings.modifiedStats == null)
            {
                CombatPowerTweakerMod.Settings.modifiedStats = new Dictionary<string, float>();
            }

            CombatPowerTweakerMod.Settings.vanillaMemory = new Dictionary<string, float>();
            CombatPowerTweakerMod.Settings.pawnKindNames = new Dictionary<string, string>();
            foreach (var pawnKind in thingsWithCombatPower)
            {
                CombatPowerTweakerMod.Settings.vanillaMemory[pawnKind.defName] = pawnKind.combatPower;
                CombatPowerTweakerMod.Settings.pawnKindNames[pawnKind.defName] = pawnKind.label;
                if (CombatPowerTweakerMod.Settings.modifiedStats.ContainsKey(pawnKind.defName))
                {
                    continue;
                }

                CombatPowerTweakerMod.Settings.modifiedStats[pawnKind.defName] = pawnKind.combatPower;
            }
        }
    }
}