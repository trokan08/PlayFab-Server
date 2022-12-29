using UnityEngine;

namespace ScriptableObjects.ConfigData
{
    [CreateAssetMenu(menuName = "Data/Config/Movement", order = 1)]

    public class CD_MovementSpeed : ScriptableObject
    {
        public MovementSpeedVO CharacterMovementProperties;
    }
}