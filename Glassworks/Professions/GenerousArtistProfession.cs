using StardewValley;

namespace Glassworks.Professions;

public class GenerousArtistProfession : SpaceCore.Skills.Skill.Profession
{
    public GenerousArtistProfession(SpaceCore.Skills.Skill skill) : base(skill, "GenerousArtist")
    {
        Icon = Game1.staminaRect;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_ArtistCapstoneA_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_ArtistCapstoneA_Description();
    }
}