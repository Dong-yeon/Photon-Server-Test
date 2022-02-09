using System.Collections.Generic;
using Photon.Hive.Plugin;

namespace MyFirstPlugin
{
    public class MyPluginFactory : IPluginFactory
    {
        public IGamePlugin Create(IPluginHost gameHost, string pluginName,
            Dictionary<string, string> config, out string errorMsg)
        {
            MyFirstPlugin plugin = new MyFirstPlugin();
            if (plugin.SetupInstance(gameHost, config, out errorMsg))
            {
                return plugin;
            }
            return null;
        }

        // plugin을 많이 사용할때 팩토리 패턴
        // pluginName에 따라 switch로 분기시켜 이름에 맞는 객체를 생성하여 반환하도록 한다.
        /*        public IGamePlugin Create(IPluginHost gameHost, string pluginName,
                    Dictionary<string, string> config, out string errorMsg)
                {
                    IGamePlugin plugin = new MyFirstPlugin(); // default
                    switch (pluginName)
                    {
                        case "Default":
                            // name not allowed, throw error
                            break;
                        case "NameOfYourPlugin":
                            plugin = new NameOfYourPlugin(); // 커스텀 플러그인
                            break;
                        case "NameOfOtherPlugin":
                            plugin = new NameOfOtherPlugin(); // 커스텀 플러그인
                            break;
                        default:
                            //plugin = new DefaultPlugin();
                            break;
                    }
                    if (plugin.SetupInstance(gameHost, config, out errorMsg))
                    {
                        return plugin;
                    }
                    return null;
            }*/
    }
}
