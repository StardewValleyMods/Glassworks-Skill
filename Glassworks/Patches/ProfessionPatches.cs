using Glassworks.Skills;
using HarmonyLib;
using SpaceCore;
using StardewValley;
using StardewValley.GameData.Machines;
using SDVObject = StardewValley.Object;

namespace Glassworks.Patches;

public static class ProfessionPatches
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(MachineDataUtility), nameof(MachineDataUtility.TryGetMachineOutputRule)),
            postfix: new HarmonyMethod(typeof(ProfessionPatches), nameof(TryGetMachineOutputRule_Postfix))
        );

        harmony.Patch(
            AccessTools.Method(typeof(SDVObject), nameof(SDVObject.OutputMachine)),
            prefix: new HarmonyMethod(typeof(ProfessionPatches), nameof(OutputMachine_Prefix)),
            postfix: new HarmonyMethod(typeof(ProfessionPatches), nameof(OutputMachine_Postfix))
        );
    }

    private static void TryGetMachineOutputRule_Postfix(
        ref bool __result,
        SDVObject machine,
        Farmer who,
        ref MachineOutputRule? rule,
        ref MachineOutputTriggerRule? triggerRule)
    {
        if (!__result || !who.IsLocalPlayer)
        {
            return;
        }

        if (rule == null || !ItemCategories.IsGlassworksMachine(machine))
        {
            return;
        }

        if (!who.HasCustomProfession(GlassworksSkill.Resourceful))
        {
            return;
        }

        if (!ItemCategories.IsGlassBlade(rule))
        {
            return;
        }

        if (triggerRule == null || triggerRule.RequiredCount < 2)
        {
            return;
        }

        triggerRule = new MachineOutputTriggerRule
        {
            Id = triggerRule.Id,
            Trigger = triggerRule.Trigger,
            RequiredItemId = triggerRule.RequiredItemId,
            RequiredTags = triggerRule.RequiredTags,
            RequiredCount = 1,
            Condition = triggerRule.Condition
        };
    }

    private static void OutputMachine_Prefix(SDVObject __instance, MachineOutputRule? outputRule, Farmer? who, bool probe)
    {
        if (probe || outputRule == null || who == null || !who.IsLocalPlayer)
        {
            return;
        }

        if (!ItemCategories.IsGlassworksMachine(__instance))
        {
            return;
        }

        if (!who.HasCustomProfession(GlassworksSkill.Proficient))
        {
            return;
        }

        if (!ItemCategories.IsReplicaSword(outputRule))
        {
            return;
        }

        if (outputRule.DaysUntilReady >= 1)
        {
            int days = outputRule.DaysUntilReady - 1;
            if (days <= 0)
            {
                outputRule.DaysUntilReady = -1;
                outputRule.MinutesUntilReady = Math.Max(720, outputRule.MinutesUntilReady);
            }
            else
            {
                outputRule.DaysUntilReady = days;
            }
        }
        else if (outputRule.MinutesUntilReady >= 1440)
        {
            outputRule.MinutesUntilReady = Math.Max(720, outputRule.MinutesUntilReady - 1440);
        }
    }

    private static void OutputMachine_Postfix(SDVObject __instance, Farmer? who, bool probe, bool __result)
    {
        if (probe || !__result || who == null || !who.IsLocalPlayer)
        {
            return;
        }

        if (!ItemCategories.IsGlassworksMachine(__instance))
        {
            return;
        }

        int level = who.GetCustomSkillLevel("Ex0dus.GlassworksSkill");
        if (level < 6)
        {
            return;
        }

        SDVObject? output = __instance.heldObject.Value;
        if (output == null)
        {
            return;
        }

        bool isQualityItem = ItemCategories.IsGlassArt(output)
            || ItemCategories.IsDeluxeArt(output)
            || ItemCategories.IsReplicaSword(output);

        if (!isQualityItem)
        {
            return;
        }

        int roll = Game1.random.Next(100);
        if (roll < level)
        {
            output.Quality = 2; // Gold
        }
        else if (roll < level + (level * 2))
        {
            output.Quality = 1; // Silver
        }
    }
}