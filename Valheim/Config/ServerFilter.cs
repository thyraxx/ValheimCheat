using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim.Config
{
    public static class ServerFilter
    {
        public static bool Enabled { get; set; } = false;
        public static bool HasPassword { get; set; } = true;

        public static int Amount { get; set; } = 200;
    }
}
