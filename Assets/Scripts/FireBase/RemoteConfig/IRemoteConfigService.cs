using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.FireBase.RemoteConfig
{
    public interface IRemoteConfigService
    {        
        string GetString(string key, string fallback = "");
        int GetInt(string key, int fallback = 0);
        float GetFloat(string key, float fallback = 0f);
        bool GetBool(string key, bool fallback = false);
    }
}
