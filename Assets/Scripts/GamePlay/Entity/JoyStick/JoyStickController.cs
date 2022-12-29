using System;
using GameManager;
using UnityEngine;

namespace GamePlay.JoyStick
{
    public class JoyStickController : MonoBehaviour
    {
        [SerializeField] private DynamicJoystick _dynamicJoystick;
        [SerializeField] private GameObject _parent;
        private Action _updatePosition;

        private void OnEnable()
        {
            _updatePosition += JoyStickMovement;
          //  GameActions.Instance.GameSucces += () => {DisableActivateJoyStick(false); };
            
        }

        private void OnDisable()
        {
            _updatePosition -= JoyStickMovement;
            //GameActions.Instance.GameSucces -= () => {DisableActivateJoyStick(false); };
        }

        private void Update()
        {
            if(_updatePosition != null)
            {
                _updatePosition.Invoke();
            }
        }
        
        
        private void JoyStickMovement()
        {
            Vector3 pos = Vector3.forward * _dynamicJoystick.Vertical + Vector3.right * _dynamicJoystick.Horizontal;
            GameActions.Instance.JoyStickDirection.Invoke(pos);
        }

        private void DisableActivateJoyStick(bool bo)
        {
            _parent.SetActive(bo);
        }
    }
}