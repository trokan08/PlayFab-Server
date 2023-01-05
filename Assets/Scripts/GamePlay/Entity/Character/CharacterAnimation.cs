using UnityEngine;

namespace Runtime.Contexts.Main.Entity
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private bool _run;

        public void Run()
        {
            if (!_run)
            {
                _run = true;
                _animator.SetTrigger("Run");    
            }
            
        }

        public void Idle()
        {
            if (_run)
            {
                _run = false;
                _animator.SetTrigger("Idle");    
            }
            
        }

    }
}