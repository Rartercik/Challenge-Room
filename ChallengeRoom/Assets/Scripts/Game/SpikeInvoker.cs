using System;
using UnityEngine;

public class SpikeInvoker : MonoBehaviour
{
    [SerializeField] Animator _spikeInvokeAnimator;
    [SerializeField] Spike _spike;

    private int _spikeDurationInSeconds;
    private Action _onDestroy;
    private bool IsPlaying => _spike.gameObject.activeSelf;

    public void StartInvoke(int seconds, Action onDestroy)
    {
        if (IsPlaying) throw new InvalidOperationException("Can not invoke invoked spike!");
        _spikeInvokeAnimator.Play("Invoke");
        _spikeDurationInSeconds = seconds;
        _onDestroy = onDestroy;
    }

    private void Invoke()
    {
        _spike.Invoke(_spikeDurationInSeconds, _onDestroy);
    }
}
