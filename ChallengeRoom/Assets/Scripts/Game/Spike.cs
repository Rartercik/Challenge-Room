using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private Animator _destroyAnimator;

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
            _destroyAnimator.Play("Destroy");
            gameObject.SetActive(false);
            onDestroy.Invoke();
        }
    }
}
