using Assets.Scripts.Menu.Levels;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Scripts.Menu.UI
{
    public class StartLevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button _button;
        public int Number { get; private set; }
        private StartGame _startGame;
        private SetupLevelSequence _setupLevel;

        private void OnEnable()
        {
            _button.onClick.AddListener(StartLevelButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(StartLevelButtonClick);
        }

        public void SetNumber(int value) => Number = value = Mathf.Clamp(value, 1, 10);

        public void SetLabel() => _label.text = Number.ToString();

        public void SetButtonInteracteble(bool value) => _button.interactable = value;


        private void StartLevelButtonClick() =>
            _startGame.Start(_setupLevel.CurrentlevelSequence.LevelSequence[Number-1]);

        [Inject]
        private void Construvt(SetupLevelSequence setupLevel, StartGame startGame)
        {
            _setupLevel = setupLevel;
            _startGame = startGame;
        }
    }
}