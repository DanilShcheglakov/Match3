using Firebase.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.FireBase.Analitycs
{
    public class GameAnalytics : IAnalyticsService
    {
        public void Initialize()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }

        public void LogLevelEnd(int levelNumber, bool success, float timeSpent)
        {
            FirebaseAnalytics.LogEvent("level_end", 
                new Parameter("level", levelNumber),
                new Parameter("success", success?1:0),
                new Parameter("time_spent", timeSpent));
        }

        public void LogLevelStart(int levelNumber)
        {
            FirebaseAnalytics.LogEvent("level_start", new Parameter ("level", levelNumber));
        }
    }
}
