using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Tools;

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

        public int GetSeconds()
        {
            return _seconds;
        }

        private IEnumerator ExecuteEverySecond()
        {
            yield return new WaitForSeconds(1);

            _seconds++;
            _text.text = TimeTool.GetFormatTime(_seconds);

            _onEverySecond(_seconds);
            StartCoroutine(ExecuteEverySecond());
        }
    }
}
