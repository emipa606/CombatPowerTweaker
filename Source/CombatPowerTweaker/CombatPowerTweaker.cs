using Verse;

namespace CombatPowerTweaker
{
    [StaticConstructorOnStartup]
    internal static class CombatPowerTweaker
    {
        static CombatPowerTweaker()
        {
            SetCombatPowerValues();
        }

        public static void SetCombatPowerValues()
        {
            var changedDefs = 0;
            foreach (var pawnKind in DefDatabase<PawnKindDef>.AllDefsListForReading)
            {
                changedDefs++;
                if (!CombatPowerTweakerMod.settings.modifiedStats.ContainsKey(pawnKind.defName) || CombatPowerTweakerMod.settings.modifiedStats[pawnKind.defName] == CombatPowerTweakerMod.settings.vanillaMemory[pawnKind.defName])
                {
                    continue;
                }
                pawnKind.combatPower = CombatPowerTweakerMod.settings.modifiedStats[pawnKind.defName];
            }
            if (changedDefs > 0)
            {
                Log.Message($"CombatPowerTweaker: Changed the combat power for {changedDefs} PawnKinds");
            }
        }
    }

}
