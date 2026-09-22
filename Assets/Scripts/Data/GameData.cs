using Assets.Scripts.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
    public class GameData
    {
        public GameData()
        {
            IsEnabledSound = true;
            CurrentLevelIndex = 1;
        }

        public LevelConfig CurrenLevel { get; private set; }
        public int CurrentLevelIndex { get; private set; }
        public bool IsEnabledSound { get; private set; }

        public void SetCurrentLevelIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            CurrentLevelIndex = index;
        }

        public void OpenNextLevel() => CurrentLevelIndex++;

        public bool SetEnabledSound(bool value) => IsEnabledSound = value;

        public void SetCurrentLevel(LevelConfig level)
        {
            if (level != null) CurrenLevel = level;
        }
    }
}
