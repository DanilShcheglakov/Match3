using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using Assets.Scripts.Menu.Levels;
using Assets.Scripts.Menu.UI;
using Assets.Scripts.SceneLoading;
using VContainer.Unity;

namespace Assets.Scripts.Menu
{
    public class MenuEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SetupLevelSequence _setupLevel;
        private LevelsSequenceView _levelsSequenceView;
        private MenuAnimations _menuAnimations;
        private AudioManager _audioManager;
        private GameData _gameData;

        public MenuEntryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequence setupLevel,
            LevelsSequenceView levelsSequenceView, MenuAnimations menuAnimations,
            AudioManager audioManager, GameData gameData)
        {
            _sceneLoading = sceneLoading;
            _setupLevel = setupLevel;
            _levelsSequenceView = levelsSequenceView;
            _menuAnimations = menuAnimations;
            _audioManager = audioManager;
            _gameData = gameData;
        }

        public async void Initialize()
        {
            await _setupLevel.Setup(_gameData.CurrentLevelIndex);
            _levelsSequenceView.SetupButtonsView(_gameData.CurrentLevelIndex);
            _audioManager.PlayMenuMusic();
            _sceneLoading.LoadingIsDone(true);
            await _menuAnimations.StartAnimation();
        }
    }
}
