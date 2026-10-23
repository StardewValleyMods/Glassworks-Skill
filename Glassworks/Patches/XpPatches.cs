using HarmonyLib;
using SpaceCore;
using StardewValley;
using SDVObject = StardewValley.Object;

namespace Glassworks.Patches;

public static class XpPatches
{
    private const string GeodeCrusherId = "(BC)182";
    private const int GeodeXp = 7;

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(Stats), "set_GeodesCracked"),
            postfix: new HarmonyMethod(typeof(XpPatches), nameof(GeodePostfix))
        );

        harmony.Patch(
            AccessTools.Method(typeof(SDVObject), "CheckForActionOnMachine"),
            postfix: new HarmonyMethod(typeof(XpPatches), nameof(GeodeCrusherPostfix))
        );
    }

    private static void GeodePostfix()
    {
        Game1.player?.AddCustomSkillExperience("Ex0dus.GlassworksSkill", GeodeXp);
    }

    private static void GeodeCrusherPostfix(SDVObject __instance, Farmer who, bool __result)
    {
        if (__instance.QualifiedItemId != GeodeCrusherId
            || !__result
            || __instance.readyForHarvest.Value)
        {
            return;
        }

        if (who.IsLocalPlayer)
        {
            Game1.player.AddCustomSkillExperience("Ex0dus.GlassworksSkill", GeodeXp);
        }
    }
}