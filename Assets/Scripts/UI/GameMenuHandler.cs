using Core;
using Ground;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameMenuHandler : MonoBehaviour
    {
        [SerializeField] private GameUIPresenter _presenter;
        [SerializeField] private GameSession _session;
        [SerializeField] private FinishGround _finishGround;
        [SerializeField] private FigureStateHandler _figureHandler;

        private void OnEnable()
        {
            _finishGround.LevelFinished += OnLevelComplete;
            _figureHandler.TimesOut += OnLevelFailed;
        }
        
        private void OnDisable()
        {
            _finishGround.LevelFinished -= OnLevelComplete;
            _figureHandler.TimesOut -= OnLevelFailed;
        }

        public void StartLevel()
        {
            _presenter.LevelStart();
            _session.StartGame();
        }

        public void RestartLevel()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
        }

        public void LoadNextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }

        private void OnLevelComplete()
        {
            _presenter.ShowLevelCompleted();
            _session.OnGameOver();
        }

        private void OnLevelFailed()
        {
            _presenter.ShowLevelFailed();
            _session.OnGameOver();
        }
    }
}