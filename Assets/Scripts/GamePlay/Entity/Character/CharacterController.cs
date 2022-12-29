using System;
using GameManager;
using PlayFab.ClientServices.GamePlayConfigs;
using PlayFab.ConfigDatas;
using ScriptableObjects.ConfigData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Contexts.Main.Entity
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Transform _visual;
        
        private float _moveSpeed ;

        private float _turnSpeed = 5f;

        public Vector3 Direction;

        public Rigidbody Rb;

        private void OnEnable()
        {
            GameActions.Instance.JoyStickDirection += SetDirection;
            CharacterMovementService characterMovementService = new CharacterMovementService();
            characterMovementService.GetCharacterMovement(SetupCharacterController);
        }

        private void OnDisable()
        {
            GameActions.Instance.JoyStickDirection -= SetDirection;

        }

        public void SetupCharacterController(CD_MovementSpeed cdMovementSpeed)
        {
            _moveSpeed = cdMovementSpeed.CharacterMovementProperties.MoveSpeed;
            _turnSpeed = cdMovementSpeed.CharacterMovementProperties.TurnSpeed;
        }

        private void SetDirection(Vector3 joyStickDirection)
        {
            Direction = joyStickDirection;
        }

        private void FixedUpdate()
        {
            Rb.velocity = Direction * _moveSpeed;

            if(Direction != Vector3.zero)
            {
                LookAtGradually(_visual,transform.position + Direction, _turnSpeed * Time.deltaTime, true);
            }
        }

        public void SetSpeed(float speed, float turnSpeed)
        {
            _moveSpeed = speed;
            _turnSpeed = turnSpeed;
        }
        
        public  void LookAtGradually(Transform transform, Vector3 target, float maxRadiansDelta, bool stableUpVector = false)
        {
            Vector3 targetDirection = target - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, maxRadiansDelta, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
            if(stableUpVector)
            {
                transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, 0f);
            }
        }
    }
}
