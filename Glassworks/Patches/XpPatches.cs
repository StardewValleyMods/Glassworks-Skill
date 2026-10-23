using HarmonyLib;
using SpaceCore;
using StardewValley;

namespace Glassworks.Patches;

public static class XpPatches
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(Stats), "set_GeodesCracked"),
            postfix: new HarmonyMethod(typeof(XpPatches), nameof(GeodePostfix))
        );
    }

    private static void GeodePostfix()
    {
        Game1.player?.AddCustomSkillExperience("Ex0dus.GlassworksSkill", 7);
    }
}