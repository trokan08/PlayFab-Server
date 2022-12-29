using GameManager;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class CoinTrigger : MonoBehaviour, ITrigger
    {
        public void Action(GameObject gameObject)
        {
            this.gameObject.SetActive(false);
            GameActions.Instance.CoinCollected.Invoke();
        }
    }
}