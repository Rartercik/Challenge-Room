using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Interface
{
    [RequireComponent(typeof(Text))]
    public class Timer : MonoBehaviour
    {
        private Text _text;
        private Action<int> _onEverySecond;
        private int _seconds;

        private void Start()
        {
            _text = GetComponent<Text>();
        }

        public void StartTimer(Action<int> onEverySecond)
        {
            gameObject.SetActive(true);
            StartCoroutine(ExecuteEverySecond());
            _onEverySecond = onEverySecond;
        }

        private IEnumerator ExecuteEverySecond()
        {
            yield return new WaitForSeconds(1);

            _seconds++;
            _text.text = GetFormatTime(_seconds);

            _onEverySecond(_seconds);
            StartCoroutine(ExecuteEverySecond());
        }

        private string GetFormatTime(int seconds)
        {
            var minutes = seconds / 60;
            var tenSeconds = seconds % 60 / 10;
            var singleSeconds = seconds % 10;

            return string.Format("{0}:{1}{2}", minutes, tenSeconds, singleSeconds);
        }
    }
}
