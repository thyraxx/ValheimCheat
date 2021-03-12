using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim
{
    [HarmonyPatch(typeof(Attack))]
    class AttackPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("UseAmmo")]
        public static bool AttackUseAmmo_PrePatch(ref Attack __instance, ref ItemDrop.ItemData ___m_ammoItem, ref Humanoid ___m_character, ref ItemDrop.ItemData ___m_weapon, ref bool __result)
        {

            if (__instance.m_attackType == Attack.AttackType.Projectile)
            {
                ItemDrop.ItemData itemData = ___m_character.GetAmmoItem();

                ___m_character.GetInventory().RemoveItem(itemData, 0);
                ___m_ammoItem = itemData;

                __result = true;
                return false;
            }

            return true;
        }
    }
}
