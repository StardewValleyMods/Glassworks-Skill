namespace Glassworks.Professions;

public class GlassmithProfession : SpaceCore.Skills.Skill.Profession
{
    public GlassmithProfession(SpaceCore.Skills.Skill skill) : base(skill, "Glassmith")
    {
        Icon = Assets.GlassmithIcon;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_Glassmith_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_Glassmith_Description(ModConfig.Instance.ArtistGlassmithDoubleChance.ToString("0%"));
    }
}