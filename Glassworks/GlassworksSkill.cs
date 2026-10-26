using Glassworks.Professions;
using Microsoft.Xna.Framework;
using SpaceCore;
using StardewModdingAPI;
using StardewModdingAPI.Events;

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

        ExperienceBarColor = new Color(120, 180, 220);
        ExperienceCurve = new[] { 100, 380, 770, 1300, 2150, 3300, 4800, 6900, 10000, 15000 };

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
        return new List<string>();
    }

    public override string GetSkillPageHoverText(int level)
    {
        return I18n.Skill_Glassworks_Hover();
    }

    public override void DoLevelPerk(int level)
    {
        // Level effects are added in later stages.
    }
}