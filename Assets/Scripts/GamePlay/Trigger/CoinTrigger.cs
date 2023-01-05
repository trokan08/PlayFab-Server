using DG.Tweening;
using GameManager;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class CoinTrigger : MonoBehaviour, ITrigger
    {
        public void Action(GameObject gameObject)
        {
            transform.DOMoveY(transform.position.y + 15, 3f).OnComplete(() => { this.gameObject.SetActive(false); });
            GameActions.Instance.CoinCollected.Invoke(1);
        }
    }
}