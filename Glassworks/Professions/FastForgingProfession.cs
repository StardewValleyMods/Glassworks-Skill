using StardewValley;

namespace Glassworks.Professions;

public class FastForgingProfession : SpaceCore.Skills.Skill.Profession
{
    public FastForgingProfession(SpaceCore.Skills.Skill skill) : base(skill, "GlassworksGlassmithCapstoneB")
    {
        Icon = Game1.staminaRect;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_GlassmithCapstoneB_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_GlassmithCapstoneB_Description();
    }
}