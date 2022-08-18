using UnityEngine;
using UnityEngine.Events;
using Interface;

namespace Game
{
    public class GameProcessor : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onGameStarted;
        [SerializeField] private AttackInstanceFactory[] _attackInstanceFactories;
        [SerializeField] private Timer _timer;

        public void StartGame()
        {
            _onGameStarted.Invoke();
            _timer.StartTimer(ExecuteEverySecond);
        }

        private void ExecuteEverySecond(int seconds)
        {
            foreach (var factory in _attackInstanceFactories)
                factory.UpdateFactory(seconds);
        }
    }
}
