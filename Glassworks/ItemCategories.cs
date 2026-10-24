using SDVObject = StardewValley.Object;

namespace Glassworks;

public static class ItemCategories
{
    public static bool IsGlassBlade(SDVObject item) => item.HasContextTag("glassworks_blade_item");

    public static bool IsGlassArt(SDVObject item) => item.HasContextTag("glassworks_art_item");

    public static bool IsDeluxeArt(SDVObject item) => item.HasContextTag("glassworks_deluxe_art_item");

    public static bool IsHilt(SDVObject item) => item.HasContextTag("glassworks_hilt_item");

    public static bool IsReplicaSword(SDVObject item) => item.HasContextTag("glassworks_sword_item");

    public static bool IsMachineProduct(SDVObject item) => IsGlassBlade(item) || IsGlassArt(item) ||
                                                           IsDeluxeArt(item) || IsHilt(item) || IsReplicaSword(item);
}
