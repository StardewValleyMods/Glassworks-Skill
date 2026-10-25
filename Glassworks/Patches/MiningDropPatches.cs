using HarmonyLib;
using Microsoft.Xna.Framework;
using SpaceCore;
using StardewValley;
using MineShaft = StardewValley.Locations.MineShaft;

namespace Glassworks.Patches;

public static class MiningDropPatches
{
    private const string SkillId = "Ex0dus.GlassworksSkill";

    private const int RequiredLevel = 8;
    private const double DoubleChance = 0.25;

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(typeof(MineShaft), nameof(MineShaft.checkStoneForItems)),
            prefix: new HarmonyMethod(typeof(MiningDropPatches), nameof(CaptureDebrisCounts)),
            postfix: new HarmonyMethod(typeof(MiningDropPatches), nameof(DoubleDroppedGeodes))
        );
    }

    private static void CaptureDebrisCounts(MineShaft __instance, out Snapshot __state)
    {
        GameLocation? current = ReferenceEquals(Game1.currentLocation, __instance) ? null : Game1.currentLocation;

        __state = new Snapshot(
            __instance,
            __instance.debris.Count,
            current,
            current?.debris.Count ?? 0
        );
    }

    private static void DoubleDroppedGeodes(MineShaft __instance, Snapshot __state, int x, int y, Farmer who)
    {
        if (who.GetCustomSkillLevel(SkillId) < RequiredLevel)
        {
            return;
        }

        List<string> geodes = new();
        CollectGeodes(__state.Shaft, __state.ShaftCount, geodes);
        if (__state.Current != null)
        {
            CollectGeodes(__state.Current, __state.CurrentCount, geodes);
        }

        if (geodes.Count == 0)
        {
            return;
        }

        if (Game1.random.NextDouble() >= DoubleChance)
        {
            return;
        }

        Vector2 origin = new Vector2(x * 64 + 32, y * 64 + 32);
        Vector2 target = who.getStandingPosition();

        foreach (string qualifiedItemId in geodes)
        {
            __instance.debris.Add(new Debris(qualifiedItemId, origin, target));
        }
    }

    private static void CollectGeodes(GameLocation location, int skipCount, List<string> results)
    {
        int index = 0;
        foreach (Debris debris in location.debris)
        {
            bool addedDuringCall = index++ >= skipCount;
            if (!addedDuringCall)
            {
                continue;
            }

            string? qualifiedItemId = debris.itemId.Value;
            if (string.IsNullOrEmpty(qualifiedItemId))
            {
                continue;
            }

            if (Utility.IsGeode(ItemRegistry.Create(qualifiedItemId)))
            {
                results.Add(qualifiedItemId);
            }
        }
    }

    private readonly struct Snapshot
    {
        public Snapshot(GameLocation shaft, int shaftCount, GameLocation? current, int currentCount)
        {
            Shaft = shaft;
            ShaftCount = shaftCount;
            Current = current;
            CurrentCount = currentCount;
        }

        public GameLocation Shaft { get; }

        public int ShaftCount { get; }

        public GameLocation? Current { get; }

        public int CurrentCount { get; }
    }
}
