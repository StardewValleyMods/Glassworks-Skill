using Microsoft.Xna.Framework;
using StardewValley;

namespace Glassworks.Skills;

public class GlassworksSkill : SpaceCore.Skills.Skill
{
    public GlassworksSkill() : base("Ex0dus.GlassworksSkill")
    {
        // Temporary placeholder icons until dedicated assets are added.
        Icon = Game1.staminaRect;
        SkillsPageIcon = Game1.staminaRect;

        ExperienceBarColor = new Color(120, 180, 220);
        ExperienceCurve = new[] { 100, 380, 770, 1300, 2150, 3300, 4000, 6900, 10000, 15000 };
    }

    public override string GetName()
    {
        return I18n.Skill_Glassworks_Name();
    }

    public override List<string> GetExtraLevelUpInfo(int level)
    {
        return new List<string>();
    }

    public override string GetSkillPageHoverText(int level)
    {
        return string.Empty;
    }

    public override void DoLevelPerk(int level)
    {
        // Level effects are added in later stages.
    }
}