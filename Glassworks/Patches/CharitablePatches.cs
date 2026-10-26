using HarmonyLib;
using SpaceCore;
using StardewValley;
using SDVObject = StardewValley.Object;

namespace Glassworks.Patches;

public static class CharitablePatches
{
    private const int FlatBonusPoints = 250;

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(NPC), nameof(NPC.receiveGift)),
            prefix: new HarmonyMethod(typeof(CharitablePatches), nameof(SnapshotFriendship)),
            postfix: new HarmonyMethod(typeof(CharitablePatches), nameof(ApplyFlatBonus))
        );
    }

    private static void SnapshotFriendship(NPC __instance, out int __state, SDVObject o, Farmer giver)
    {
        __state = 0;

        if (!giver.HasCustomProfession(GlassworksSkill.Charitable) || !ItemCategories.IsArtItem(o))
        {
            return;
        }

        if (giver.friendshipData.TryGetValue(__instance.Name, out Friendship friendship))
        {
            __state = friendship.Points;
        }
    }

    private static void ApplyFlatBonus(NPC __instance, int __state, SDVObject o, Farmer giver)
    {
        if (!giver.HasCustomProfession(GlassworksSkill.Charitable) || !ItemCategories.IsArtItem(o))
        {
            return;
        }

        if (!giver.friendshipData.TryGetValue(__instance.Name, out Friendship friendship))
        {
            return;
        }

        int vanillaAddedPoints = friendship.Points - __state;
        int netAdjustment = FlatBonusPoints - vanillaAddedPoints;

        giver.changeFriendship(netAdjustment, __instance);
    }
}