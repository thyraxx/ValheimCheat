using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim
{
    [HarmonyPatch(typeof(Humanoid))]
    class HumanoidPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("DrainEquipedItemDurability")]
        public static void DrainEquipedItemDurability_PrePatch(ItemDrop.ItemData item, float dt)
        {
            item.m_durability = item.GetMaxDurability();
        }
    }
}
