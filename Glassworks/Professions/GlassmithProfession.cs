using StardewValley;

namespace Glassworks.Professions;

public class GlassmithProfession : SpaceCore.Skills.Skill.Profession
{
    public GlassmithProfession(SpaceCore.Skills.Skill skill) : base(skill, "GlassworksGlassmith")
    {
        Icon = Game1.staminaRect;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_Glassmith_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_Glassmith_Description();
    }
}