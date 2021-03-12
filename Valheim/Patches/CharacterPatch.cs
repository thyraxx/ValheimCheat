using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim
{
    [HarmonyPatch(typeof(Character))]
    class CharacterPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("UpdateSwiming")]
        public static void CharacterUpdateSwiming_PostPatch(ref float ___m_swimSpeed, ref float ___m_swimAcceleration)
        {
            if (SwimStuff.Enabled)
            {
                SwimStuff.CustomSpeed = 10f;
                SwimStuff.CustomAcceleration = 1f;
            }
            else
            {
                SwimStuff.CustomSpeed = 2f;
                SwimStuff.CustomAcceleration = 0.05f;
            }

            ___m_swimSpeed = SwimStuff.CustomSpeed;
            ___m_swimAcceleration = SwimStuff.CustomAcceleration;
        }
    }
}
