using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Valheim
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class ValheimMod : BaseUnityPlugin
    {
        public const string pluginGuid = "valheimmod.myfirstmod";
        public const string pluginName = "My first valheim!";
        public const string pluginVersion = "0.0.1";
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
