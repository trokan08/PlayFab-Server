using System.Collections;
using PlayFab.ClientServices.GameAnalytics;
using UnityEngine;

namespace GamePlay.EntityClass
{
    public class Timer
    {
        private float _time = 0;
        private IEnumerator _timer;
        public void StartTimer(MonoBehaviour mono)
        {
            _time = 0;
            _timer = TimeCount();
            mono.StartCoroutine(_timer);
        }

        private IEnumerator TimeCount()
        {
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
            
            
            while (true)
            {
                _time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
        }

        public LevelVO GetTime(MonoBehaviour mono)
        {
            mono.StopCoroutine(_timer);
            int minutes = ((int)_time) / 60;
            int seconds = ((int)_time) % 60;
            LevelVO level = new LevelVO();
            level.Minute = minutes;
            level.Seconds = seconds;
            return level;

        }
        
        
    }
}