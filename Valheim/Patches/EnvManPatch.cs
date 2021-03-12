using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim
{
    [HarmonyPatch(typeof(EnvMan))]
    class EnvManPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch("IsFreezing")]
        public static bool EnvManIsFreezing_PrePatch(ref bool __result)
        {
            __result = false;
            return false;
        }
    }
}
