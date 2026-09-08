using Assets.Scripts.Menu.Levels;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Menu.UI
{
    public class LevelsSequenceView : MonoBehaviour
    {

        [SerializeField] private  List<StartLevelButton> _levelButtons = new List<StartLevelButton>();

        private SetupLevelSequence _setupLevel;

        [Inject] private void Construct(SetupLevelSequence setupLevel)
        {
            _setupLevel = setupLevel;
        }

        private void OnValidate()
        {
            if (_levelButtons.Count != 5)
                throw new ArgumentOutOfRangeException("Level buttons must contain 5 elements");
        }

        public void SetupButtonsView(int currentLevel)
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].SetNumber(_setupLevel.CurrentlevelSequence.LevelSequence[i].LevelNumber);
                _levelButtons[i].SetLabel();

                if (_levelButtons[i].Number> currentLevel)                
                    _levelButtons[i].SetButtonInteracteble(false);                
            }
        }
    }
}