using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;

namespace TeleportUtil
{
    public class TeleportUtilConfig : IRocketPluginConfiguration
    {
        public IRocketPluginConfiguration DefaultConfiguration
        {
            get
            { 
                return new TeleportUtilConfig()
                {

                };
            }
        }
    }
}
