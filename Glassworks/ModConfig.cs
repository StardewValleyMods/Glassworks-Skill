using Microsoft.Xna.Framework;

namespace Glassworks;

public class ModConfig
{
    public static ModConfig Instance { get; set; } = null!;

    public Color ExperienceBarColor = new (120, 180, 220);
    public int[] ExperienceCurve = { 100, 380, 770, 1300, 2150, 3300, 4800, 6900, 10000, 15000 };

    public int GeodeXp = 10;
    public int GlassArtXp = 10;
    public int GlassDeluxeArtXp = 15;
    public int GlassBladeXp = 25;
    public int GlassHiltXp = 10;
    public int ReplicaSwordXp = 40;

    public double ArtistGlassmithDoubleChance = 0.4;
    public double DoubleGeodeChance = 0.25;

    public float MasterpiecePriceBoost = 1.50f;
    public float Level9PriceBoost = 1.25f;
}