using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Hive.Plugin;


namespace MyFirstPlugin
{
    public class MyPluginFactory : PluginFactoryBase // 기본 클래스를 PluginFactoryBase로 변경
    {

        public override IGamePlugin CreatePlugin(string pluginName)
        {
            // throw new NotImplementedException();
            return new MyFirstPlugin(); // 플러그인을 생성하고 리턴
        }
    }
}
