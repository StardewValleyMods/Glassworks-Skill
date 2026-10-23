using StardewValley;

namespace Glassworks.Professions;

public class ProficientProfession : SpaceCore.Skills.Skill.Profession
{
    public ProficientProfession(SpaceCore.Skills.Skill skill) : base(skill, "Proficient")
    {
        Icon = Game1.staminaRect;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_Proficient_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_Proficient_Description();
    }
}