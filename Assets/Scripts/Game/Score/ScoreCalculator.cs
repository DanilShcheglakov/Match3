using Assets.Scripts.Game.MatchedTiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Game.Score
{
    public class ScoreCalculator
    {
        private GameProgress _gameProgress;

        public ScoreCalculator(GameProgress gameProgress)
        {
            _gameProgress = gameProgress;
        }

        public void CalculateScoreToAdd(MatchDirection matchDirection)
        {
            switch (matchDirection)
            {
                case MatchDirection.Horizontal:
                case MatchDirection.Vertical:
                    _gameProgress.AddScore(20);
                    Debug.Log("+20 Score");
                    break;
                case MatchDirection.LongHorizontal:
                case MatchDirection.LongVertical:
                    _gameProgress.AddScore(50);
                    Debug.Log("+50 Score");
                    break;
                case MatchDirection.Multiply:
                    _gameProgress.AddScore(200);
                    Debug.Log("+200 Score");
                    break;
            }
        }
    }
}
