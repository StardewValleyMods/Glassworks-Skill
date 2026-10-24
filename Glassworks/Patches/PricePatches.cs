using Glassworks.Skills;
using HarmonyLib;
using SpaceCore;
using StardewValley;
using SDVObject = StardewValley.Object;

namespace Glassworks.Patches;

public static class PricePatches
{
    private const string SkillId = "Ex0dus.GlassworksSkill";

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(SDVObject), "getPriceAfterMultipliers"),
            postfix: new HarmonyMethod(typeof(PricePatches), nameof(PricePostfix))
        );
    }

    public static void PricePostfix(
        SDVObject __instance,
        ref float __result,
        long specificPlayerID)
    {
        if (__result <= 0f
            || !(specificPlayerID == -1L || Game1.player.UniqueMultiplayerID == specificPlayerID))
        {
            return;
        }

        if (ItemCategories.IsMachineProduct(__instance))
        {
            int level = Game1.player.GetCustomSkillLevel(SkillId);
            if (level >= 10 && Game1.player.HasCustomProfession(GlassworksSkill.Masterpiece))
            {
                __result  *= 1.50f;
            }
            else if (level >= 9)
            {
                __result *= 1.25f;
            }
        }
    }
}
