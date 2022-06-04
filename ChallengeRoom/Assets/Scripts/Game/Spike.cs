using System;
using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] Animator _destroyAnimator;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.StartDying();
        }
    }

    public void Invoke(int seconds, Action onDestroy)
    {
        gameObject.SetActive(true);
        StartCoroutine(DeleteAfter(seconds, onDestroy));
    }

    private IEnumerator DeleteAfter(int seconds, Action onDestroy)
    {
        yield return new WaitForSeconds(seconds);
        onDestroy.Invoke();
        _destroyAnimator.Play("Destroy");
        gameObject.SetActive(false);
    }
}
