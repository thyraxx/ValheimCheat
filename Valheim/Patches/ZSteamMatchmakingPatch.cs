using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valheim.Config;

namespace Valheim.Patches
{
    [HarmonyPatch(typeof(ZSteamMatchmaking))]
    class ZSteamMatchmakingPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("FilterServers")]
        public static bool ZSteamMatchmakingFilterServers_PrePatch(List<ServerData> input, List<ServerData> allServers, ref string ___m_nameFilter)
        {
            string text = ___m_nameFilter.ToLowerInvariant();
            foreach (ServerData serverData in input)
            {
                if (serverData.m_password == ServerFilter.HasPassword)
                {
                    if (text.Length == 0 || serverData.m_name.ToLowerInvariant().Contains(text))
                    {
                        allServers.Add(serverData);
                    }

                    if(allServers.Count >= ServerFilter.Amount)
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
