using UnityEngine;
using TMPro;
using Tools;

namespace Interface
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class BestTime : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        [SerializeField] private string _defaultText;

        private readonly string _key = "Time";

        private TextMeshProUGUI _text;
        private int _record;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();

            if (PlayerPrefs.HasKey(_key))
            {
                _record = PlayerPrefs.GetInt(_key);
                _text.text = _defaultText + TimeTool.GetFormatTime(_record);
            }
        }

        public void UpdateRecord()
        {
            var seconds = _timer.GetSeconds();

            if (seconds > _record)
            {
                _record = seconds;
                PlayerPrefs.SetInt(_key, _record);
            }
        }
    }
}