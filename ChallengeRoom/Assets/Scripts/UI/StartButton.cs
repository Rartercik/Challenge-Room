using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] Game _game;
        [SerializeField] UnityEvent OnGameStarted;

        public void StartGame()
        {
            _game.StartGame();
            OnGameStarted.Invoke();
        }
    }
}
