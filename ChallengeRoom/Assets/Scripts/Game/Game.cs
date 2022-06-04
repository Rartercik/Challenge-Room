using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] UnityEvent _onGameStarted;
    [SerializeField] AttackInstanceFactory[] _attackInstanceFactories;
    [SerializeField] Timer _timer;

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
