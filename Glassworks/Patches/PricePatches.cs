using HarmonyLib;
using SpaceCore;
using StardewValley;

namespace Glassworks.Patches;

public static class PricePatches
{
    private const string SkillId = "Ex0dus.GlassworksSkill";

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(StardewValley.Object), "getPriceAfterMultipliers"),
            postfix: new HarmonyMethod(typeof(PricePatches), nameof(PricePostfix))
        );
    }

    public static void PricePostfix(
        StardewValley.Object __instance,
        ref float __result,
        long specificPlayerID)
    {
        if (__result <= 0f
            || !(specificPlayerID == -1L || Game1.player.UniqueMultiplayerID == specificPlayerID))
        {
            return;
        }

        if (Game1.player.GetCustomSkillLevel(SkillId) >= 9 && ItemCategories.IsMachineProduct(__instance))
        {
            __result *= 1.25f;
        }
    }
}
