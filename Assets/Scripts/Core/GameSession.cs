using UnityEngine;

namespace Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private GameState _currentGameState;

        private void Start()
        {
            WaitingToStart();
        }
        
        public void StartGame()
        {
            _currentGameState = GameState.Started;
            EnablePlayerControl();
            Time.timeScale = 1;
        }
        
        public void WaitingToStart()
        {
            _currentGameState = GameState.WaitingToStart;
            DisablePlayerControl();
            Time.timeScale = 0;
        }
        
        public void OnGameOver()
        {
            _currentGameState = GameState.GameOver;
            DisablePlayerControl();
            Time.timeScale = 0;
        }

        private void PauseGame()
        {
            _currentGameState = GameState.Paused;
            DisablePlayerControl();
            Time.timeScale = 0;
        }
        
        private void EnablePlayerControl()
        {
            _player.SetActive(true);
        }

        private void DisablePlayerControl()
        {
            _player.SetActive(false);
        }
    }
}