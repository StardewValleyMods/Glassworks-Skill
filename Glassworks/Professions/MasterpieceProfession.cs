using StardewValley;

namespace Glassworks.Professions;

public class MasterpieceProfession : SpaceCore.Skills.Skill.Profession
{
    public MasterpieceProfession(SpaceCore.Skills.Skill skill) : base(skill, "Masterpiece")
    {
        Icon = Game1.staminaRect;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_ArtistCapstoneB_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_ArtistCapstoneB_Description();
    }
}