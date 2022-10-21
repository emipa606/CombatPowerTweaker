using Mlie;
using UnityEngine;
using Verse;

namespace CombatPowerTweaker;

internal class CombatPowerTweakerMod : Mod
{
    private static CombatPowerTweakerSettings settings;
    public static string currentVersion;

    public CombatPowerTweakerMod(ModContentPack pack)
        : base(pack)
    {
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(
                ModLister.GetActiveModWithIdentifier("Mlie.CombatPowerTweaker"));
    }

    public static CombatPowerTweakerSettings Settings
    {
        get
        {
            if (settings == null)
            {
                settings = LoadedModManager.GetMod<CombatPowerTweakerMod>()
                    .GetSettings<CombatPowerTweakerSettings>();
            }

            return settings;
        }
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        Settings.DoSettingsWindowContents(inRect);
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