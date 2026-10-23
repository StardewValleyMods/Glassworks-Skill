using StardewValley;

namespace Glassworks.Professions;

public class ResourcefulProfession : SpaceCore.Skills.Skill.Profession
{
    public ResourcefulProfession(SpaceCore.Skills.Skill skill) : base(skill, "Resourceful")
    {
        Icon = Game1.staminaRect;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_Resourceful_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_Resourceful_Description();
    }
}