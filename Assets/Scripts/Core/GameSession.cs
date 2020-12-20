using System;
using Ground;
using Player;
using UnityEngine;

namespace Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private FinishGround _finish;

        private GameState _currentGameState;

        private void OnEnable()
        {
            _finish.LevelFinished += OnLevelFinished;
        }

        private void OnDisable()
        {
            _finish.LevelFinished -= OnLevelFinished;
        }

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

        public void PauseGame()
        {
            _currentGameState = GameState.Paused;
            DisablePlayerControl();
            Time.timeScale = 0;
        }

        private void WaitingToStart()
        {
            _currentGameState = GameState.WaitingToStart;
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

        private void OnLevelFinished()
        {
            print("Level Finished");
        }
    }
}