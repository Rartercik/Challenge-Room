using System.Collections;
using UnityEngine;

namespace Tools
{
    public static class TimeTool
    {
        public static string GetFormatTime(int seconds)
        {
            var minutes = seconds / 60;
            var tenSeconds = seconds % 60 / 10;
            var singleSeconds = seconds % 10;

            return string.Format("{0}:{1}{2}", minutes, tenSeconds, singleSeconds);
        }
    }
}