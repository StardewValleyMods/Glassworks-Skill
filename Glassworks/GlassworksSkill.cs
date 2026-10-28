using Glassworks.Professions;
using Microsoft.Xna.Framework;
using SpaceCore;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Glassworks;

public class GlassworksSkill : Skills.Skill
{
    public const string SkillId = "Ex0dus.GlassworksSkill";

    private readonly IModHelper _helper;

    public static ArtistProfession Artist { get; private set; } = null!;
    public static GlassmithProfession Glassmith { get; private set; } = null!;
    public static CharitableProfession Charitable { get; private set; } = null!;
    public static MasterpieceProfession Masterpiece { get; private set; } = null!;
    public static ResourcefulProfession Resourceful { get; private set; } = null!;
    public static ProficientProfession Proficient { get; private set; } = null!;

    public GlassworksSkill(IModHelper helper) : base(SkillId)
    {
        _helper = helper;
        _helper.Events.GameLoop.DayStarted += GameLoopOnDayStarted;

        ExperienceBarColor = ModConfig.Instance.ExperienceBarColor;
        ExperienceCurve = ModConfig.Instance.ExperienceCurve;

        Artist = new ArtistProfession(this);
        Glassmith = new GlassmithProfession(this);
        Charitable = new CharitableProfession(this);
        Masterpiece = new MasterpieceProfession(this);
        Resourceful = new ResourcefulProfession(this);
        Proficient = new ProficientProfession(this);

        Professions.Add(Artist);
        Professions.Add(Glassmith);
        ProfessionsForLevels.Add(new ProfessionPair(5, Artist, Glassmith));

        Professions.Add(Charitable);
        Professions.Add(Masterpiece);
        ProfessionsForLevels.Add(new ProfessionPair(10, Charitable, Masterpiece, Artist));

        Professions.Add(Resourceful);
        Professions.Add(Proficient);
        ProfessionsForLevels.Add(new ProfessionPair(10, Resourceful, Proficient, Glassmith));
    }

    private void GameLoopOnDayStarted(object? sender, DayStartedEventArgs e)
    {
        Icon = Assets.GlassworksIcon;
        SkillsPageIcon = Assets.GlassworksIconSmall;

        _helper.Events.GameLoop.DayStarted -= GameLoopOnDayStarted;
    }

    public override string GetName()
    {
        return I18n.Skill_Glassworks_Name();
    }

    public override List<string> GetExtraLevelUpInfo(int level)
    {
        List<string> list = new();
        if (level > 9)
        {
            return list;
        }

        switch (level)
        {
            case 4:
                list.Add(I18n.Skill_Glassworks_LevelUp_4());
                break;
            case 6:
                list.Add(I18n.Skill_Glassworks_LevelUp_6_Gold());
                list.Add(I18n.Skill_Glassworks_LevelUp_6_Silver());
                break;
            case 8:
                list.Add(I18n.Skill_Glassworks_LevelUp_8(ModConfig.Instance.DoubleGeodeChance.ToString("0%")));
                break;
            case 9:
                list.Add(I18n.Skill_Glassworks_LevelUp_9((ModConfig.Instance.Level9PriceBoost - 1f).ToString("0%")));
                break;
        }

        return list;
    }
    public override string GetSkillPageHoverText(int level)
    {
        return I18n.Skill_Glassworks_Hover();
    }
}