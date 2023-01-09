using GameManager;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class ObstacleTrigger : MonoBehaviour, ITrigger
    {
        public void Action(GameObject gameObject)
        {
            GameActions.Instance.CoinCollected.Invoke(-1);
        }
    }
}