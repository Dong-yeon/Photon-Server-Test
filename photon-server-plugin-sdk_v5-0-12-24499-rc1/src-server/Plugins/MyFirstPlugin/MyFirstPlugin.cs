using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Hive.Plugin; // PluginBase를 기본 클래스로 변경하기위해서 using

namespace MyFirstPlugin
{
    public class MyFirstPlugin : PluginBase
    {
        public override string Name => "MyFirstPlugin";

        private IPluginLogger pluginLogger;

        public override bool SetupInstance(IPluginHost host, Dictionary<string, string> config, out string errorMsg)
        {
            this.pluginLogger = host.CreateLogger(this.Name);
            return base.SetupInstance(host, config, out errorMsg);
        }

        public override void OnCreateGame(ICreateGameCallInfo info)
        {
            this.pluginLogger.InfoFormat("OnCreateGame {0} by user {1}", info.Request.GameId, info.UserId);
            info.Continue(); // same as base.OnCreateGame(info);
        }
    }
}
