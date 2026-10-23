using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace Glassworks;

public static class Assets
{
    public static Texture2D AuraciteIcon => Game1.content.Load<Texture2D>("Mods/Ex0dus.Glassworks/Auracite");

    private static Texture2D? _auraciteIconSmall;

    public static Texture2D AuraciteIconSmall
    {
        get
        {
            if (_auraciteIconSmall == null)
            {
                Texture2D source = AuraciteIcon;
                _auraciteIconSmall = new Texture2D(Game1.graphics.GraphicsDevice, 10, 10);
                Color[] data = new Color[10 * 10];
                source.GetData(0, new Rectangle(3, 3, 10, 10), data, 0, data.Length);
                _auraciteIconSmall.SetData(data);
            }

            return _auraciteIconSmall;
        }
    }
}