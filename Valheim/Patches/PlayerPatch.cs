using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim
{
    [HarmonyPatch(typeof(Player))]
    class PlayerPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("AutoPickup")]
        public static void AutoPickupRange_PrePatch(ref float ___m_autoPickupRange)
        {
            ___m_autoPickupRange = 6f;
        }

        [HarmonyPrefix]
        [HarmonyPatch("UpdateStats")]
        public static void UpdateStats_PrePatch(ref float ___m_stamina, ref float ___m_maxStamina)
        {
            ___m_stamina = ___m_maxStamina;
        }

        [HarmonyPrefix]
        [HarmonyPatch("IsEncumbered")]
        public static bool PlayerIsEncumbered_PrePatch(ref bool __result)
        {
            __result = false;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("ActivateGuardianPower")]
        public static bool PlayerActivateGuardianPower_PrePatch(ref StatusEffect ___m_guardianSE, ref Player ___m_localPlayer)
        {
            ___m_localPlayer.GetSEMan().AddStatusEffect(___m_guardianSE.name, true);
            List<Player> list = new List<Player>();
            Player.GetPlayersInRange(___m_localPlayer.transform.position, 10f, list);
            foreach (Player player in list)
            {
                player.GetSEMan().AddStatusEffect(___m_guardianSE.name, true);
            }

            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch("UpdateMovementModifier")]
        public static void PlayerUpdateMovementModifier_PostPatch(ref float ___m_equipmentMovementModifier)
        {
            ___m_equipmentMovementModifier = 0f;
        }
    }
}
