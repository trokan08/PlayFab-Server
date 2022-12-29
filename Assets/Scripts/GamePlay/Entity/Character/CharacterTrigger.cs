using System;
using GameManager;
using GamePlay.Trigger;
using UnityEngine;

namespace Runtime.Contexts.Main.Entity
{
    public class CharacterTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            ITrigger trigger =  other.GetComponent<ITrigger>();
            
            if(trigger != null){
                
                other.GetComponent<ITrigger>().Action(this.gameObject);

            }
        }
    }
}