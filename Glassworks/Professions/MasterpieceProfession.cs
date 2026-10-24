namespace Glassworks.Professions;

public class MasterpieceProfession : SpaceCore.Skills.Skill.Profession
{
    public MasterpieceProfession(SpaceCore.Skills.Skill skill) : base(skill, "Masterpiece")
    {
        Icon = Assets.MasterpieceIcon;
    }

    public override string GetName()
    {
        return I18n.Profession_Glassworks_Masterpiece_Name();
    }

    public override string GetDescription()
    {
        return I18n.Profession_Glassworks_Masterpiece_Description();
    }
}