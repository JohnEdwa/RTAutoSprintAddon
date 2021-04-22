# RTAutoSprintExtended Custom Survivor/Skill Addon | Game ver. 1.1.1.4

---

## Contact

Open an issue [at the Github repo](https://github.com/JohnEdwa/RTAutoSprintExtended) or find me on the RoR2 modding discord (JohnEdwa#7903).

## Changelog

`1.0.0`  [2021-04-xx]

* First Release.

# Description

Additional patch for RTAutoSprintExtended that add patches for a few custom characters and skill packs, as well as a way to set them up as a configuration file entry.

### Mod compatibility and "API":

Included patches:

* Artificer Extended: Animation delay for ``IceShard``, ``LaserBolt`` and ``SnowBall``.
* MandoGaming: Animation delay for ``HeavyPistol2`` and ``BeamPistol``.
* EggsSkills: Animation delay for ``CombatShotgunEntity``, ``TeslaMineFireState``. Sprint disable for ``DirectiveRoot``.
* Playble Templar: Sprint disable for ``TemplarRifleFire``.
* The House: Sprint disable for ``Roulette``.

**This is for RTAutoSprintEx, i.e what this addon is doing.**

You can use SendMessage to register an EntityState to the list of Sprint Disablers and Animation Delayers. 
Add a soft dependency to ensure RTAutoSprintEx is loaded before your mod.

```
[BepInDependency("com.johnedwa.RTAutoSprintEx", BepInDependency.DependencyFlags.SoftDependency)]

if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.johnedwa.RTAutoSprintEx")) {
    SendMessage("RT_SprintDisableMessage", "EntityStates.Mage.Weapon.Flamethrower"); 
    SendMessage("RT_AnimationDelayMessage", "EntityStates.Mage.Weapon.FireFireBolt"); 
}
```

`RT_SprintDisableMessage`  blocks AutoSprinting from activating when the player is in that EntityState.
`RT_AnimationDelayMessage` looks for a field called `duration` to use as a delay - useful for keeping wind-down animations from being immediately cancelled. 