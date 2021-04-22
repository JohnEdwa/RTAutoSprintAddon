using BepInEx;
using BepInEx.Configuration;
using System.Linq;
using System;

namespace RTAutoSprintEx {

    [BepInDependency("com.johnedwa.RTAutoSprintEx", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Borbo.ArtificerExtended", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.FMRadio11.MandoGaming", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Egg.EggsSkills", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Tymmey.Templar", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.JavAngle.HouseMod", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.johnedwa.RTAutoSprintAddon", "RTAutoSprintAddon", "1.0.0")]

    public class RTAutoSprintAddon : BaseUnityPlugin {
        public void Awake() {

            ConfigFile conf = new ConfigFile(Paths.ConfigPath + "\\RTAutoSprintAddon.cfg", true);
            ConfigEntry<bool> EnableDebugLogConf = conf.Bind<bool>(
                "1) Options", "EnableDebugLog", true, 
                new ConfigDescription("Print debug logs to the console.",
                new AcceptableValueList<bool>(true, false)));
            ConfigEntry<bool> ArtificerExtendedConf = conf.Bind<bool>(
                "2) Included Patches", "ArtificerExtended", true, 
                new ConfigDescription("Enables the Artificer Extended compatibility patch.",
                new AcceptableValueList<bool>(true, false)));
            ConfigEntry<bool> MandoGamingConf = conf.Bind<bool>(
                "2) Included Patches", "MandoGaming", true, 
                new ConfigDescription("Enables the Mando Gaming compatibility patch.",
                new AcceptableValueList<bool>(true, false)));
            ConfigEntry<bool> EggsSkillsConf = conf.Bind<bool>(
                "2) Included Patches", "EggsSkills", true, 
                new ConfigDescription("Enables the EggsSkills compatibility patch.",
                new AcceptableValueList<bool>(true, false)));
            ConfigEntry<bool> PlayableTemplarConf = conf.Bind<bool>(
                "2) Included Patches", "PlayableTemplar", true, 
                new ConfigDescription("Enables the Playable Templar compatibility patch.",
                new AcceptableValueList<bool>(true, false)));
            ConfigEntry<bool> TheHouseConf = conf.Bind<bool>(
                "2) Included Patches", "TheHouse", true, 
                new ConfigDescription("Enables The House compatibility patch.",
                new AcceptableValueList<bool>(true, false)));
            ConfigEntry<string> DisablerConf = conf.Bind<string>(
                "3) Manual Patching", "SprintDisableEntityStates", "", 
                new ConfigDescription("List of EntityStates that disable sprinting."));
            ConfigEntry<string> DelayerConf = conf.Bind<string>(
                "3) Manual Patching", "AnimationDelayEntityStates", "", 
                new ConfigDescription("List of EntityStates that check for `duration` field for a delay."));

            char[] delimiterChars = { ' ', ',' };
			string[] entityStateDisableList = DisablerConf.Value.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            string[] entityStateDelayList = DelayerConf.Value.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.johnedwa.RTAutoSprintEx")) {

                if (EnableDebugLogConf.Value) Logger.LogInfo("Adding RTAutoSprintEx custom skill/survivor patches: ");
                if (entityStateDisableList.Count() > 0) {
                    foreach (var state in entityStateDisableList)  {
                        if (EnableDebugLogConf.Value) Logger.LogInfo("Adding Custom EntityState Disabler for " + state);
                        SendMessage("RT_RegisterSprintDisable", state);

                    }
                }
                if (entityStateDelayList.Count() > 0) {
                    foreach (var state in entityStateDelayList)  {
                        if (EnableDebugLogConf.Value) Logger.LogInfo("Adding Custom EntityState Delayer for " + state);
                        SendMessage("RT_RegisterAnimationDelay", state);
                    }
                }

                // Artificer Extended
                if (ArtificerExtendedConf.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Borbo.ArtificerExtended")) {
                    if (EnableDebugLogConf.Value) Logger.LogInfo("...Artificer Extended.");
                    SendMessage("RT_RegisterAnimationDelay", "AltArtificerExtended.EntityStates.FireIceShard");
                    SendMessage("RT_RegisterAnimationDelay", "AltArtificerExtended.EntityStates.FireLaserbolts");
                    SendMessage("RT_RegisterAnimationDelay", "AltArtificerExtended.EntityStates.FireSnowBall");
                    SendMessage("RT_RegisterSprintDisable", "AltArtificerExtended.EntityStates.CastThunder");
                }

                // MandoGaming
                if (MandoGamingConf.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.FMRadio11.MandoGaming")) {
                    if (EnableDebugLogConf.Value) Logger.LogInfo("...MandoGaming.");
                    SendMessage("RT_RegisterAnimationDelay", "FMCommando.Skills.HeavyPistol2");
                    SendMessage("RT_RegisterAnimationDelay", "FMCommando.Skills.BeamPistol");
                }

                //EggsSkills
                if (EggsSkillsConf.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Egg.EggsSkills")) {
                    if (EnableDebugLogConf.Value) Logger.LogInfo("...EggsSkills.");
                    SendMessage("RT_RegisterSprintDisable", "EggsSkills.EntityStates.DirectiveRoot");
                    SendMessage("RT_RegisterAnimationDelay", "EggsSkills.EntityStates.CombatShotgunEntity");
                    SendMessage("RT_RegisterAnimationDelay", "EggsSkills.EntityStates.TeslaMineFireState");
                }

                //Playble Templar
                if (PlayableTemplarConf.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Tymmey.Templar")) {
                    if (EnableDebugLogConf.Value) Logger.LogInfo("...Playable Templar");
                    SendMessage("RT_RegisterSprintDisable", "Templar.TemplarRifleFire");
                }

                //The House
                if (ArtificerExtendedConf.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.JavAngle.HouseMod")) {
                    if (EnableDebugLogConf.Value) Logger.LogInfo("...The House.");
                    SendMessage("RT_RegisterAnimationDelay", "HouseMod2.SkillStates.Roulette");
                }
                if (EnableDebugLogConf.Value) Logger.LogInfo("Patching completed.");
            }
        }
    }
}