using GenericModConfigMenu;
using Microsoft.Xna.Framework;
using StardewModdingAPI;

namespace Glassworks;

public class ModConfig
{
    public static ModConfig Instance { get; set; } = null!;

    public Color ExperienceBarColor = new (120, 180, 220);
    public int[] ExperienceCurve = { 100, 380, 770, 1300, 2150, 3300, 4800, 6900, 10000, 15000 };

    public int GeodeXp = 5;
    public int GlassArtXp = 20;
    public int GlassDeluxeArtXp = 40;
    public int GlassBladeXp = 10;
    public int GlassHiltXp = 10;
    public int ReplicaSwordXp = 40;

    public double ArtistGlassmithDoubleChance = 0.25;
    public double DoubleGeodeChance = 0.25;

    public float MasterpiecePriceBoost = 1.50f;
    public float Level9PriceBoost = 1.25f;

    public static void RegisterMenu(IModHelper helper, IManifest manifest)
    {
        IGenericModConfigMenuApi? configMenu = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
        if (configMenu is null)
        {
            return;
        }

        configMenu.Register(
            mod: manifest,
            reset: () => Instance = new ModConfig(),
            save: () => helper.WriteConfig(Instance)
        );

        configMenu.AddSectionTitle(manifest, I18n.Config_Experience_Title);

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_GeodeXp_Name,
            tooltip: I18n.Config_GeodeXp_Tooltip,
            getValue: () => Instance.GeodeXp,
            setValue: value => Instance.GeodeXp = value,
            min: 0,
            max: 500,
            interval: 1
        );

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_GlassArtXp_Name,
            tooltip: I18n.Config_GlassArtXp_Tooltip,
            getValue: () => Instance.GlassArtXp,
            setValue: value => Instance.GlassArtXp = value,
            min: 0,
            max: 500,
            interval: 1
        );

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_GlassDeluxeArtXp_Name,
            tooltip: I18n.Config_GlassDeluxeArtXp_Tooltip,
            getValue: () => Instance.GlassDeluxeArtXp,
            setValue: value => Instance.GlassDeluxeArtXp = value,
            min: 0,
            max: 500,
            interval: 1
        );

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_GlassBladeXp_Name,
            tooltip: I18n.Config_GlassBladeXp_Tooltip,
            getValue: () => Instance.GlassBladeXp,
            setValue: value => Instance.GlassBladeXp = value,
            min: 0,
            max: 500,
            interval: 1
        );

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_GlassHiltXp_Name,
            tooltip: I18n.Config_GlassHiltXp_Tooltip,
            getValue: () => Instance.GlassHiltXp,
            setValue: value => Instance.GlassHiltXp = value,
            min: 0,
            max: 500,
            interval: 1
        );

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_ReplicaSwordXp_Name,
            tooltip: I18n.Config_ReplicaSwordXp_Tooltip,
            getValue: () => Instance.ReplicaSwordXp,
            setValue: value => Instance.ReplicaSwordXp = value,
            min: 0,
            max: 500,
            interval: 1
        );

        // Bonus chances.
        configMenu.AddSectionTitle(manifest, I18n.Config_Chances_Title);

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_ArtistGlassmithDoubleChance_Name,
            tooltip: I18n.Config_ArtistGlassmithDoubleChance_Tooltip,
            getValue: () => (float)Instance.ArtistGlassmithDoubleChance,
            setValue: value => Instance.ArtistGlassmithDoubleChance = value,
            min: 0f,
            max: 1f,
            interval: 0.05f,
            formatValue: value => $"{value:P0}"
        );

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_DoubleGeodeChance_Name,
            tooltip: I18n.Config_DoubleGeodeChance_Tooltip,
            getValue: () => (float)Instance.DoubleGeodeChance,
            setValue: value => Instance.DoubleGeodeChance = value,
            min: 0f,
            max: 1f,
            interval: 0.05f,
            formatValue: value => $"{value:P0}"
        );

        // Price boosts.
        configMenu.AddSectionTitle(manifest, I18n.Config_Prices_Title);

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_MasterpiecePriceBoost_Name,
            tooltip: I18n.Config_MasterpiecePriceBoost_Tooltip,
            getValue: () => Instance.MasterpiecePriceBoost,
            setValue: value => Instance.MasterpiecePriceBoost = value,
            min: 1f,
            max: 5f,
            interval: 0.05f,
            formatValue: value => $"{value:0.00}x"
        );

        configMenu.AddNumberOption(
            mod: manifest,
            name: I18n.Config_Level9PriceBoost_Name,
            tooltip: I18n.Config_Level9PriceBoost_Tooltip,
            getValue: () => Instance.Level9PriceBoost,
            setValue: value => Instance.Level9PriceBoost = value,
            min: 1f,
            max: 5f,
            interval: 0.05f,
            formatValue: value => $"{value:0.00}x"
        );
    }
}