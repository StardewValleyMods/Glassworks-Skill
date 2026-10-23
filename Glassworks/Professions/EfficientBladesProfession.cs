using StardewValley;

namespace Glassworks.Professions;

public class EfficientBladesProfession : SpaceCore.Skills.Skill.Profession
{
    public EfficientBladesProfession(SpaceCore.Skills.Skill skill) : base(skill, "EfficientBlades")
    {
        Icon = Game1.staminaRect;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_GlassmithCapstoneA_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_GlassmithCapstoneA_Description();
    }
}