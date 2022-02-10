using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Hive.Plugin;

namespace MyCompany.MyProject.HivePlugin
{
    public class CustomPlugin : PluginBase
    {
        public override string Name
        {
            get
            {
                return "CustomPlugin"; // anything other than "Default" or "ErrorPlugin"
            }
        }
    }
}
