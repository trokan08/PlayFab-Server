using System;
using GameManager;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class FinishTrigger : MonoBehaviour, ITrigger
    {
        private bool _isFirsTime = true;

        private void OnEnable()
        {
            _isFirsTime = true;
        }

        private void OnDisable()
        {
            _isFirsTime = false;
        }

        public void Action(GameObject gameObject)
        {
            if (_isFirsTime)
            {
                _isFirsTime = false;
                GameActions.Instance.SaveGame.Invoke();
                GameActions.Instance.GameSucces.Invoke();
            }
            
            
        }
        
        
    }
}