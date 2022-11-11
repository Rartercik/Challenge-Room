using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class DestroyingObject : MonoBehaviour
    {
        [SerializeField] private GameObject _blinkingObject;
        [SerializeField] private float _startDestroyingTime;
        [SerializeField] private float _destroyingTime;
        [SerializeField] private float _blinkingTime;

        private IEnumerator _blinkingCorountine;

        private void Awake()
        {
            _blinkingCorountine = BlinkAfter(_blinkingTime);
        }

        public void StartDestroying(Action onDestroying, DestroyingType destroyingType)
        {
            gameObject.SetActive(true);
            _blinkingObject.SetActive(true);
            StartCoroutine(StartDestroying(_startDestroyingTime, _destroyingTime, onDestroying, destroyingType));
        }

        private IEnumerator StartDestroying(float startDestroyingTime, float destroyingTime, Action onDestroying, DestroyingType destroyingType)
        {
            yield return new WaitForSeconds(startDestroyingTime);

            StartCoroutine(_blinkingCorountine);
            StartCoroutine(DestroyAfter(destroyingTime, onDestroying, destroyingType));
        }

        private IEnumerator DestroyAfter(float seconds, Action onDestroying, DestroyingType destroyingType)
        {
            yield return new WaitForSeconds(seconds);

            onDestroying?.Invoke();
            StopCoroutine(_blinkingCorountine);
            Destroy(destroyingType);
        }

        private void Destroy(DestroyingType destroyingType)
        {
            if (destroyingType == DestroyingType.Destroying)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private IEnumerator BlinkAfter(float interval)
        {
            yield return new WaitForSeconds(interval);

            Blink(_blinkingObject);
            _blinkingCorountine = BlinkAfter(interval);
            StartCoroutine(_blinkingCorountine);
        }

        private void Blink(GameObject target)
        {
            var active = target.activeSelf;
            target.SetActive(!active);
        }
    }
}