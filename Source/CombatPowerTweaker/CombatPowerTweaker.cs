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
                if (!CombatPowerTweakerMod.settings.modifiedStats.ContainsKey(pawnKind.defName))
                {
                    continue;
                }
                if (CombatPowerTweakerMod.settings.modifiedStats[pawnKind.defName] == CombatPowerTweakerMod.settings.vanillaMemory[pawnKind.defName])
                {
                    continue;
                }
                pawnKind.combatPower = CombatPowerTweakerMod.settings.modifiedStats[pawnKind.defName];
                changedDefs++;
            }
            if (changedDefs > 0)
            {
                Log.Message($"CombatPowerTweaker: Changed the combat power for {changedDefs} PawnKinds");
            }
        }
    }

}
