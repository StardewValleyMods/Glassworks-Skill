using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;

namespace Glassworks;

public static class Assets
{
    private static IModHelper _helper = null!;

    public static void Init(IModHelper helper)
    {
        _helper = helper;
    }

    private static Texture2D LoadIcon(string fileName) => _helper.ModContent.Load<Texture2D>($"assets/{fileName}");

    public static Texture2D GlassworksIcon => LoadIcon("Skillicon.png");

    public static Texture2D ArtistIcon => LoadIcon("Artist.png");

    public static Texture2D GlassmithIcon => LoadIcon("Glassmith.png");

    public static Texture2D CharitableIcon => LoadIcon("Charitable.png");

    public static Texture2D MasterpieceIcon => LoadIcon("Masterpiece.png");

    public static Texture2D ResourcefulIcon => LoadIcon("Resourceful.png");

    public static Texture2D ProficientIcon => LoadIcon("Proficient.png");

    private static Texture2D? _glassworksIconSmall;

    public static Texture2D GlassworksIconSmall
    {
        get
        {
            if (_glassworksIconSmall == null)
            {
                Texture2D source = GlassworksIcon;
                _glassworksIconSmall = new Texture2D(Game1.graphics.GraphicsDevice, 10, 10);
                Color[] data = new Color[10 * 10];
                source.GetData(0, new Rectangle(3, 3, 10, 10), data, 0, data.Length);
                _glassworksIconSmall.SetData(data);
            }

            return _glassworksIconSmall;
        }
    }
}