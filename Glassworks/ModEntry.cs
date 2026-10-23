using Glassworks.Patches;
using Glassworks.Skills;
using HarmonyLib;
using SpaceCore;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Glassworks;

public class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        I18n.Init(helper.Translation);

        Harmony harmony = new(ModManifest.UniqueID);
        XpPatches.Apply(harmony);

        helper.Events.GameLoop.GameLaunched += OnGameLaunched;

        // Debug helper for testing XP during development.
        helper.ConsoleCommands.Add(
            "glassworks_addxp",
            "Add Glassworking XP. Usage: glassworks_addxp <amount>",
            (command, args) =>
            {
                if (args.Length < 1 || !int.TryParse(args[0], out int amount))
                {
                    Monitor.Log("Usage: glassworks_addxp <amount>", LogLevel.Info);
                    return;
                }

                Game1.player.AddCustomSkillExperience("Ex0dus.GlassworksSkill", amount);
                Monitor.Log($"Added {amount} Glassworking XP.", LogLevel.Info);
            });
    }

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        SpaceCore.Skills.RegisterSkill(new GlassworksSkill(Helper));
        Monitor.Log("Registered Glassworking skill.");
    }
}