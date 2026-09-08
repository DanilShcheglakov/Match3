using Assets.Scripts.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Menu.Levels
{
    [CreateAssetMenu(fileName = "LevelSequnce", menuName = "Configs/LevelSequence")]
    public class LevelSequnce : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levelSequence = new List<LevelConfig>();

        public List<LevelConfig> LevelSequence => _levelSequence;

        private void OnValidate()
        {
            if (_levelSequence.Count!=5)            
                throw new ArgumentOutOfRangeException("Levels sequence must contain 5 elements");            
        }
    }
}
