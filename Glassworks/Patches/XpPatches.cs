using HarmonyLib;
using SpaceCore;
using StardewValley;
using SDVObject = StardewValley.Object;

namespace Glassworks.Patches;

public static class XpPatches
{
    private const string SkillId = "Ex0dus.GlassworksSkill";

    private const string GeodeCrusherId = "(BC)182";
    private const string KilnId = "(BC)Ex0dus.Glassworks_Kiln";
    private const string DeluxeKilnId = "(BC)Ex0dus.Glassworks_Deluxe_Kiln";
    private const string ArtificerId = "(BC)Ex0dus.Glassworks_Artificer";

    private const int GeodeXp = 10;
    private const int GlassArtXp = 10;
    private const int GlassDeluxeArtXp = 15;
    private const int GlassBladeXp = 25;
    private const int GlassHiltXp = 10;
    private const int ReplicaSwordXp = 40;

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(Stats), "set_GeodesCracked"),
            postfix: new HarmonyMethod(typeof(XpPatches), nameof(GeodePostfix))
        );

        harmony.Patch(
            AccessTools.Method(typeof(SDVObject), "CheckForActionOnMachine"),
            prefix: new HarmonyMethod(typeof(XpPatches), nameof(CaptureReadyOutput)),
            postfix: new HarmonyMethod(typeof(XpPatches), nameof(MachineHarvestPostfix))
        );
    }

    private static void GeodePostfix()
    {
        Game1.player?.AddCustomSkillExperience(SkillId, GeodeXp);
    }

    private static void MachineHarvestPostfix(SDVObject __instance, Farmer who, bool __result, SDVObject? __state)
    {
        if (!__result
            || __instance.readyForHarvest.Value
            || !who.IsLocalPlayer
            || __state == null)
        {
            return;
        }

        int? xp = __instance.QualifiedItemId switch
        {
            GeodeCrusherId => GeodeXp,
            KilnId when ItemCategories.IsGlassBlade(__state) => GlassBladeXp,
            KilnId when ItemCategories.IsGlassArt(__state) => GlassArtXp,
            DeluxeKilnId when ItemCategories.IsDeluxeArt(__state) => GlassDeluxeArtXp,
            ArtificerId when ItemCategories.IsReplicaSword(__state) => ReplicaSwordXp,
            ArtificerId when ItemCategories.IsHilt(__state) => GlassHiltXp,
            _ => null,
        };
        if (xp is { } amount)
        {
            Game1.player.AddCustomSkillExperience(SkillId, amount);
        }
    }

    private static void CaptureReadyOutput(SDVObject __instance, out SDVObject? __state)
    {
        if (__instance.QualifiedItemId is GeodeCrusherId or KilnId or DeluxeKilnId or ArtificerId
            && __instance.readyForHarvest.Value)
        {
            __state = __instance.heldObject.Value;
        }
        else
        {
            __state = null;
        }
    }
}