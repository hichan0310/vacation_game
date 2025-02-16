using UnityEngine;

namespace GameBackend
{
    public static class TimeManager
    {
        public static float timeRate = 1;

        public static float deltaTime
        {
            get { return Time.deltaTime * timeRate; }
        }
    }
}