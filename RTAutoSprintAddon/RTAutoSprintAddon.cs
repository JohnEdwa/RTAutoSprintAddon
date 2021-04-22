using BepInEx;
namespace RTAutoSprintEx {
    [BepInDependency("com.johnedwa.RTAutoSprintEx", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Borbo.ArtificerExtended", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.FMRadio11.MandoGaming", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Egg.EggsSkills", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Tymmey.Templar", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.JavAngle.HouseMod", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.johnedwa.RTAutoSprintExAddon", "RTAutoSprintExAddon", "1.0.0")]

    public class RTAutoSprintAddon : BaseUnityPlugin {

        public void Awake() {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.johnedwa.RTAutoSprintEx")) {
                Logger.LogInfo("Adding RTAutoSprintEx custom skill/survivor patches: ");
                // Artificer Extended
                if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Borbo.ArtificerExtended")) {
                    Logger.LogInfo("...Artificer Extended.");
                    SendMessage("RT_RegisterAnimationDelay", "AltArtificerExtended.EntityStates.FireIceShard");
                    SendMessage("RT_RegisterAnimationDelay", "AltArtificerExtended.EntityStates.FireLaserbolts");
                    SendMessage("RT_RegisterAnimationDelay", "AltArtificerExtended.EntityStates.FireSnowBall");
                }

                // MandoGaming
                if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.FMRadio11.MandoGaming")) {
                    Logger.LogInfo("...MandoGaming.");
                    SendMessage("RT_RegisterAnimationDelay", "FMCommando.Skills.HeavyPistol2");
                    SendMessage("RT_RegisterAnimationDelay", "FMCommando.Skills.BeamPistol");
                }

                //EggsSkills
                if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Egg.EggsSkills")) {
                    Logger.LogInfo("...EggsSkills.");
                    SendMessage("RT_RegisterSprintDisable", "EggsSkills.EntityStates.DirectiveRoot");
                    SendMessage("RT_RegisterAnimationDelay", "EggsSkills.EntityStates.CombatShotgunEntity");
                    SendMessage("RT_RegisterAnimationDelay", "EggsSkills.EntityStates.TeslaMineFireState");
                }

                //Playble Templar
                if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Tymmey.Templar")) {
                    Logger.LogInfo("...Playable Templar");
                    SendMessage("RT_RegisterSprintDisable", "Templar.TemplarRifleFire");
                }

                //The House
                if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.JavAngle.HouseMod")) {
                    Logger.LogInfo("...The House.");
                    SendMessage("RT_RegisterAnimationDelay", "HouseMod2.SkillStates.Roulette");
                }
                Logger.LogInfo("Patching completed.");
            }
        }
    }
}