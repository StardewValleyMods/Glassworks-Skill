namespace Glassworks.Professions;

public class ArtistProfession : SpaceCore.Skills.Skill.Profession
{
    public ArtistProfession(SpaceCore.Skills.Skill skill) : base(skill, "Artist")
    {
        Icon = Assets.ArtistIcon;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_Artist_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_Artist_Description(ModConfig.Instance.ArtistGlassmithDoubleChance.ToString("0%"));
    }
}