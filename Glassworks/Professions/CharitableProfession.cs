using StardewValley;

namespace Glassworks.Professions;

public class CharitableProfession : SpaceCore.Skills.Skill.Profession
{
    public CharitableProfession(SpaceCore.Skills.Skill skill) : base(skill, "Charitable")
    {
        Icon = Assets.CharitableIcon;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_Charitable_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_Charitable_Description();
    }
}