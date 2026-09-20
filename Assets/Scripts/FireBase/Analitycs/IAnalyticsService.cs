using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.FireBase.Analitycs
{
    public interface IAnalyticsService
    {
        void Initialize();
        void LogLevelStart(int levelNumber);
        void LogLevelEnd(int levelNumber, bool success, float timeSpent);
    }
}
