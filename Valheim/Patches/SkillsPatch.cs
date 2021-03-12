using HarmonyLib;

namespace Valheim
{
    [HarmonyPatch(typeof(Skills))]
    class SkillsPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("OnDeath")]
        public static bool SkillsOnDeath_PrePatch()
        {
            Console.print("Died, not lowering skills.");
            return false;
        }
    }
}
