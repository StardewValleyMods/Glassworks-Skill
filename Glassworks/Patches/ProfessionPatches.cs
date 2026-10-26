using Glassworks.Skills;
using HarmonyLib;
using SpaceCore;
using StardewValley;
using StardewValley.GameData.Machines;
using SDVObject = StardewValley.Object;

namespace Glassworks.Patches;

public static class ProfessionPatches
{
    private const string KilnId = "(BC)Ex0dus.Glassworks_Kiln";
    private const string ArtificerId = "(BC)Ex0dus.Glassworks_Artificer";

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(MachineDataUtility), nameof(MachineDataUtility.TryGetMachineOutputRule)),
            postfix: new HarmonyMethod(typeof(ProfessionPatches), nameof(TryGetMachineOutputRule_Postfix))
        );

        harmony.Patch(
            AccessTools.Method(typeof(SDVObject), nameof(SDVObject.OutputMachine)),
            prefix: new HarmonyMethod(typeof(ProfessionPatches), nameof(OutputMachine_Prefix))
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

        if (machine.QualifiedItemId != KilnId || rule == null)
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

        if (__instance.QualifiedItemId != ArtificerId)
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
}