using HarmonyLib;
using SpaceCore;
using StardewValley;
using SDVObject = StardewValley.Object;

namespace Glassworks.Patches;

public static class MachinePatches
{
    private const string GeodeCrusherId = "(BC)182";

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(Stats), "set_GeodesCracked"),
            postfix: new HarmonyMethod(typeof(MachinePatches), nameof(GeodePostfix))
        );

        harmony.Patch(
            AccessTools.Method(typeof(SDVObject), "CheckForActionOnMachine"),
            prefix: new HarmonyMethod(typeof(MachinePatches), nameof(CaptureReadyOutput)),
            postfix: new HarmonyMethod(typeof(MachinePatches), nameof(MachineHarvestPostfix))
        );
    }

    private static void GeodePostfix()
    {
        Game1.player?.AddCustomSkillExperience(GlassworksSkill.SkillId, ModConfig.Instance.GeodeXp);
    }

    private static void CaptureReadyOutput(SDVObject __instance, out SDVObject? __state, bool justCheckingForActivity)
    {
        if (!justCheckingForActivity && __instance.readyForHarvest.Value && (__instance.QualifiedItemId == GeodeCrusherId || ItemCategories.IsGlassworksMachine(__instance)))
        {
            __state = __instance.heldObject.Value;
        }
        else
        {
            __state = null;
        }
    }

    private static void MachineHarvestPostfix(SDVObject __instance, bool __result, SDVObject? __state, Farmer who, bool justCheckingForActivity)
    {
        if (justCheckingForActivity || !__result || !who.IsLocalPlayer || __state == null)
        {
            return;
        }

        AwardMachineXp(__instance, who, __state);
        CheckDoubleOutput(who, __state);
    }

    private static void AwardMachineXp(SDVObject instance, Farmer who, SDVObject output)
    {

        int? xp = instance.QualifiedItemId == GeodeCrusherId
            ? ModConfig.Instance.GeodeXp
            : ItemCategories.IsGlassworksMachine(instance)
                ? ItemCategories.IsGlassBlade(output) ? ModConfig.Instance.GlassBladeXp
                : ItemCategories.IsGlassArt(output) ? ModConfig.Instance.GlassArtXp
                : ItemCategories.IsDeluxeArt(output) ? ModConfig.Instance.GlassDeluxeArtXp
                : ItemCategories.IsReplicaSword(output) ? ModConfig.Instance.ReplicaSwordXp
                : ItemCategories.IsHilt(output) ? ModConfig.Instance.GlassHiltXp
                : null
            : null;

        if (xp is { } amount)
        {
            who.AddCustomSkillExperience(GlassworksSkill.SkillId, amount);
        }
    }

    private static void CheckDoubleOutput(Farmer who, SDVObject output)
    {
        bool artistExtra = who.HasCustomProfession(GlassworksSkill.Artist) && ItemCategories.IsArtItem(output);
        bool glassmithExtra = who.HasCustomProfession(GlassworksSkill.Glassmith) && ItemCategories.IsHilt(output);

        if ((artistExtra || glassmithExtra) && Game1.random.NextDouble() < ModConfig.Instance.ArtistGlassmithDoubleChance)
        {
            SDVObject duplicate = (SDVObject)output.getOne();
            duplicate.Stack = output.Stack;

            GiveItemToFarmer(who, duplicate);
        }
    }

    private static void GiveItemToFarmer(Farmer who, SDVObject item)
    {
        if (!who.addItemToInventoryBool(item, false))
        {
            who.dropItem(item);
        }
    }
}