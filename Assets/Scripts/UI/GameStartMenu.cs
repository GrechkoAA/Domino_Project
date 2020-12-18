using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStartMenu : MonoBehaviour
    {
        [SerializeField] private GameSession _gameSession;
        [SerializeField] private GameObject _buttonStart;
        
        public void StartGame()
        {
            _buttonStart.SetActive(false);
            _gameSession.StartGame();
        }
    }
}