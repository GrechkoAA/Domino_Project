using UnityEngine;

namespace UI
{
    public class GameUIPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _levelStartUI;
        [SerializeField] private GameObject _levelCompleteUI;
        [SerializeField] private GameObject _levelFailedUI;
        
        public void LevelStart()
        {
            _levelStartUI.SetActive(false);
        }

        public void ShowLevelFailed()
        {
            _levelFailedUI.SetActive(true);
        }

        public void ShowLevelCompleted()
        {
            _levelCompleteUI.SetActive(true);
        }
    }
}