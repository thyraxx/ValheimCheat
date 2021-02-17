using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim
{
    public class SwimStuff
    {
        private float customSpeed = 2f;
        private float customAcceleration = 0.05f;
        private bool enabled = false;

        public float CustomSpeed {
            get { return customSpeed; }
            set { customSpeed = value; }
        }
        public float CustomAcceleration {
            get { return customAcceleration; }
            set { customAcceleration = value; }
        }

        public bool Enabled {
            get { return enabled; }
            set { enabled = value; }
        }
    }
}
