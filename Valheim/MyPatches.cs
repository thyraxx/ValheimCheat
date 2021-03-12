using HarmonyLib;
using System.Collections.Generic;
using static ZNet;

namespace Valheim
{

    [HarmonyPatch]
    class MyPatches
    {
        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(Minimap), "UpdatePlayerPins")]
        //public static bool MinimapLoadMapData_PrePatch(float dt, ref List<ZNet.PlayerInfo> ___m_tempPlayerInfo, ref List<Minimap.PinData> ___m_playerPins, ref Minimap __instance)
        //{
        //    ___m_tempPlayerInfo.Clear();
        //    ZNet.instance.GetOtherPublicPlayers(___m_tempPlayerInfo);
        //    List<Player> players = Player.GetAllPlayers();
        //    foreach(Player play in players)
        //    {
        //        //Console.print(play.m_name + " : " + play.m_position);
        //        //Console.print(play.GetPlayerName() + " : " + play.GetCenterPoint());
        //    }

        //    //Console.print(___m_publicReferencePosition);
        //    //___m_publicReferencePosition = true;

        //    foreach (Minimap.PinData pin in ___m_playerPins)
        //    {
        //        __instance.RemovePin(pin);
        //    }
        //    ___m_playerPins.Clear();
        //    foreach (ZNet.PlayerInfo playerInfo in ___m_tempPlayerInfo)
        //    {
        //        Minimap.PinData item = __instance.AddPin(Vector3.zero, Minimap.PinType.Player, "", false, false);
        //        ___m_playerPins.Add(item);
        //    }

        //    for (int i = 0; i < ___m_tempPlayerInfo.Count; i++)
        //    {
        //        Minimap.PinData pinData = ___m_playerPins[i];
        //        ZNet.PlayerInfo playerInfo2 = ___m_tempPlayerInfo[i];
        //        if (pinData.m_name == playerInfo2.m_name)
        //        {
        //            pinData.m_pos = Vector3.MoveTowards(pinData.m_pos, playerInfo2.m_position, 200f * dt);
        //        }
        //        else
        //        {
        //            pinData.m_name = playerInfo2.m_name;
        //            pinData.m_pos = players[0].GetCenterPoint();
        //        }
        //    }

        //    //Console.print("Name: " + players[1].GetPlayerName() + " Position: " + players[1].GetCenterPoint());

        //    //Console.print(___m_tempPlayerInfo[0].m_name);

        //    return false;
        //}

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZNet), "GetOtherPublicPlayers")]
        public static bool ZNetGetOtherPublicPlayers_PrePatch(List<ZNet.PlayerInfo> playerList, ref List<ZNet.PlayerInfo> ___m_players, ref ZDOID ___m_characterID)
        {
            foreach (ZNet.PlayerInfo playerInfo in ___m_players)
            {
                    ZDOID characterID = playerInfo.m_characterID;
                    if (!characterID.IsNone() && !(playerInfo.m_characterID == ___m_characterID))
                    {
                        PlayerInfo info = playerInfo;
                        info.m_publicPosition = true;
                        playerList.Add(playerInfo);
                    }
            }
            return false;
        }
    }
}
