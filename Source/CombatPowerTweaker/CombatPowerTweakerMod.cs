using Mlie;
using UnityEngine;
using Verse;

namespace CombatPowerTweaker;

internal class CombatPowerTweakerMod : Mod
{
    public static string CurrentVersion;

    public CombatPowerTweakerMod(ModContentPack pack)
        : base(pack)
    {
        CurrentVersion =
            VersionFromManifest.GetVersionFromModMetaData(pack.ModMetaData);
    }

    public static CombatPowerTweakerSettings Settings
    {
        get
        {
            field ??= LoadedModManager.GetMod<CombatPowerTweakerMod>()
                .GetSettings<CombatPowerTweakerSettings>();

            return field;
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