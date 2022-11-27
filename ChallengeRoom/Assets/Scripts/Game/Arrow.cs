using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _startEffect;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Transform _effectTransform;
        [SerializeField] private float _lifetime;

        [Space(30)]
        [Header("Required Components:")]
        [Space(5)]
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;

        public event Action<Arrow> OnSwitchedOff;

        private Coroutine _switchingOff;

        [Button]
        private void SetRequiredComponents()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public Transform Transform => _transform;
        public Rigidbody2D Rigidbody => _rigidbody;

        private void OnEnable()
        {
            _switchingOff = StartCoroutine(SwitchOffAfter(_lifetime));
            _startEffect.Play();
            SwitchEffectOn(_effect, _effectTransform, Transform);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.TryApplyDamage();
                SwitchOff(_effect, _effectTransform);
            }
        }

        private IEnumerator SwitchOffAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            SwitchOff(_effect, _effectTransform);
        }

        private void SwitchOff(ParticleSystem effect, Transform effectTransform)
        {
            if (_switchingOff != null)
            {
                StopCoroutine(_switchingOff);
            }

            effectTransform.SetParent(null);
            effect.Stop();
            gameObject.SetActive(false);
            OnSwitchedOff?.Invoke(this);
        }

        private void SwitchEffectOn(ParticleSystem effect, Transform effectTransform, Transform effectParent)
        {
            effectTransform.SetParent(effectParent);
            effect.Play();
        }
    }
}
