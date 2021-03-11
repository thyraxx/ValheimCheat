using HarmonyLib;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static ItemDrop.ItemData;

namespace Valheim
{

    [HarmonyPatch]
    class MyPatches
    {
        public static SwimStuff swim = new SwimStuff();
        public static Graves graves = new Graves();

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Skills), "OnDeath")]
        public static bool Skills_PrePatch()
        {
            Console.print("Died, not lowering skills.");
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Inventory), "UpdateTotalWeight")]
        public static bool UpdateTotalWeight_PrePatch(ref float ___m_totalWeight)
        {
            ___m_totalWeight = 0f;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "AutoPickup")]
        public static void AutoPickupRange_PostPatch(ref float ___m_autoPickupRange)
        {
            ___m_autoPickupRange = 6f;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "UpdateStats")]
        public static void UpdateStats_PrePatch(ref float ___m_stamina, ref float ___m_maxStamina)
        {
            ___m_stamina = ___m_maxStamina;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "IsEncumbered")]
        public static bool PlayerIsEncumbered_PrePatch(ref bool __result)
        {
            __result = false;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Humanoid), "DrainEquipedItemDurability")]
        public static void DrainEquipedItemDurability_PrePatch(ItemDrop.ItemData item, float dt)
        {
            item.m_durability = item.GetMaxDurability();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Console), "InputText")]
        public static void InputText_PostPatch(ref Console __instance)
        {

            string text = global::Console.instance.m_input.text;
            if (text.StartsWith("exploremap"))
            {
                Minimap.instance.ExploreAll();
                return;
            }

            if (text.StartsWith("swim"))
            {
                swim.Enabled = !swim.Enabled;
                __instance.Print("Swim is " + (swim.Enabled ? "enabled" : "disabled"));
                return;
            }

            // Wasn't working properly in multiplayer
            //if (text.StartsWith("delgraves"))
            //{
                //graves.Enabled = !graves.Enabled;
                //__instance.Print("Deleting your own graves is " + (graves.Enabled ? "enabled" : "disabled"));
                //return;
            //}
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Character), "UpdateSwiming")]
        public static void CharacterUpdateSwiming_PostPatch(ref float ___m_swimSpeed, ref float ___m_swimAcceleration)
        {
            if (swim.Enabled)
            {
                swim.CustomSpeed = 10f;
                swim.CustomAcceleration = 1f;
            }
            else
            {
                swim.CustomSpeed = 2f;
                swim.CustomAcceleration = 0.05f;
            }

            ___m_swimSpeed = swim.CustomSpeed;
            ___m_swimAcceleration = swim.CustomAcceleration;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Attack), "UseAmmo")]
        public static bool AttackUseAmmo_PrePatch(ref Attack __instance, ref ItemDrop.ItemData ___m_ammoItem, ref Humanoid ___m_character, ref ItemDrop.ItemData ___m_weapon, ref bool __result)
        {

            if (__instance.m_attackType == Attack.AttackType.Projectile)
            {
                ___m_ammoItem = null;
                ItemDrop.ItemData itemData = ___m_character.GetAmmoItem();

                ___m_character.GetInventory().RemoveItem(itemData, 0);
                ___m_ammoItem = itemData;

                __result = true;
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Inventory), "IsTeleportable")]
        public static bool InventoryIsTeleportable_PrePatch(ref bool __result)
        {
            __result = true;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Sign), "UpdateText")]
        public static void SignUpdateText_PrePatch(ref Text ___m_textWidget)
        {
            ___m_textWidget.color = Color.red;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "UpdateGuardianPower")]
        public static void PlayerActivateGuardianPower_PrePatch(ref float ___m_guardianPowerCooldown)
        {
            ___m_guardianPowerCooldown = 0f;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EnvMan), "IsFreezing")]
        public static bool EnvManIsFreezing_PrePatch(ref bool __result)
        {
            __result = false;
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "UpdateMovementModifier")]
        public static void CharacterUpdateWalking_PrePatch(ref float ___m_equipmentMovementModifier)
        {
            ___m_equipmentMovementModifier = 0f;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Inventory), MethodType.Constructor, new Type[] { typeof(string), typeof(Sprite), typeof(int), typeof(int) })]
        public static void InventoryConstructor_PrePatch(string name, Sprite bkg, int w, int h, ref int ___m_height)
        {
            if (name.Equals("Inventory"))
                ___m_height = 10;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Inventory), "MoveInventoryToGrave")]
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
