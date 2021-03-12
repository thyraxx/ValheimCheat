using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valheim.Config;

namespace Valheim
{
    [HarmonyPatch(typeof(Console))]
    class ConsolePatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("InputText")]
        public static void InputText_PrePatch(ref Console __instance)
        {

            string text = global::Console.instance.m_input.text;
            
            if (text.StartsWith("exploremap"))
            {
                Minimap.instance.ExploreAll();
                return;
            }

            if (text.StartsWith("swim"))
            {
                SwimStuff.Enabled = !SwimStuff.Enabled;
                __instance.Print("Swim is " + (SwimStuff.Enabled ? "enabled" : "disabled"));
                return;
            }



            if (text.StartsWith("serveronlypublic"))
            {
                ServerFilter.Enabled = !ServerFilter.Enabled;
                __instance.Print("Show only public servers filter is " + (ServerFilter.Enabled ? "enabled" : "disabled"));
                return;
            }

            if (text.StartsWith("serverlimit"))
            {
                string[] serverLimitAmount = text.Split(' ');

                if(Int32.TryParse(serverLimitAmount[1], out int i))
                {
                    ServerFilter.Amount = i;
                    __instance.Print("serverlimit amount is set to " + ServerFilter.Amount);

                }
                else
                {
                    __instance.Print(serverLimitAmount[1] + " Couldn't be parsed, is this a number? Use: serverlimit <number>");
                }

                return;
            }

            if (text.StartsWith("hasserverpassword"))
            {
                string[] serverHasPassword = text.Split(' ');

                if (Boolean.TryParse(serverHasPassword[1], out bool result))
                {
                    Console.print(result);

                    ServerFilter.HasPassword = result;
                }
                else
                {
                    __instance.Print(serverHasPassword[1] + " Couldn't be parsed, is this a boolean? Use: hasserverpassword <boolean>");
                }

                return;
            }

            //Wasn't working properly in multiplayer
            //if (text.StartsWith("delgraves"))
            //{
            //    graves.Enabled = !graves.Enabled;
            //    __instance.Print("Deleting your own graves is " + (graves.Enabled ? "enabled" : "disabled"));
            //    return;
            //}
        }
    }
}
