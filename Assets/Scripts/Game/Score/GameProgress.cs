using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Game.Score
{
    public class GameProgress
    {
        public void LoadLevelConfig(int goalScore, int moves)
        {
            Score = 0;
            GoalScore = goalScore;
            Moves = moves;
        }

        public event Action OnScoreChanged;
        public event Action OnMove;

        public int Score { get; private set; }
        public int GoalScore { get; private set; }
        public int Moves { get; private set; }

        public void AddScore(int value)
        {
            if (value == 0) throw new ArgumentOutOfRangeException(nameof(value));

            Score += value;
            OnScoreChanged?.Invoke();
            Debug.Log("---------------------Score:" + Score);

        }

        public bool CheckGoalScore() => Score >= GoalScore;
        public void SpendMoves()
        {
            Moves--;
            OnMove?.Invoke();
            Debug.Log("Moves to loose:" + Moves);
        }
    }
}
