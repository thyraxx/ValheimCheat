using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Valheim
{
    [BepInPlugin(pluginGuid, "ValheimCheat", "0.1.1")]
    public class ValheimMod : BaseUnityPlugin
    {
        public const string pluginGuid = "valheimmod.thyraxx.cheat";
        Harmony harmony = new Harmony(pluginGuid);

        public new static ManualLogSource Logger;
        public void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("Valheim Loaded!");

            harmony.PatchAll();
        }

        private void OnDestroy()
        {
            Logger.LogInfo("Valheim Unloaded!");
            harmony.UnpatchSelf();
        }
    }
}
