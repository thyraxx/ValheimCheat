using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Valheim
{
    [HarmonyPatch(typeof(Inventory))]
    class InventoryPatch
    {

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, new Type[] { typeof(string), typeof(Sprite), typeof(int), typeof(int) })]
        public static void InventoryConstructor_PostPatch(string name, Sprite bkg, int w, int h, ref int ___m_height)
        {
            if (name.Equals("Inventory"))
                ___m_height = 10;
        }

        [HarmonyPrefix]
        [HarmonyPatch("IsTeleportable")]
        public static bool InventoryIsTeleportable_PrePatch(ref bool __result)
        {
            __result = true;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("MoveInventoryToGrave")]
        public static bool InventoryMoveInventoryToGrave_PrePatch(ref Inventory original, ref Action ___m_onChanged, ref int ___m_width, ref int ___m_height, ref List<ItemDrop.ItemData> ___m_inventory)
        {
            ___m_inventory.Clear();
            ___m_width = 8;
            ___m_height = 4;

            foreach (ItemDrop.ItemData itemData in original.GetAllItems())
            {
                if (!itemData.m_shared.m_questItem && !itemData.m_equiped)
                {
                    ___m_inventory.Add(itemData);
                }
            }
            original.m_onChanged();
            ___m_onChanged();
            return false;
        }
    }
}
