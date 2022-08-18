using UnityEngine;
using UnityEngine.Events;
using Game;

namespace Interface
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private GameProcessor _game;
        [SerializeField] private UnityEvent OnGameStarted;

        public void StartGame()
        {
            _game.StartGame();
            OnGameStarted.Invoke();
        }
    }
}
