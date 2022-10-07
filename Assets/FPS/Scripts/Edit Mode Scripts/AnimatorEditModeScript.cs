using UnityEngine;

namespace FPS.Scripts.Edit_Mode_Scripts
{
    [ExecuteInEditMode]
    public class AnimatorEditModeScript : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float _DeltaTime = 0;
        private Animator _Animator = null;

        void Update()
        {
            if (Application.isPlaying)
                return;
        
            if (_Animator == null)
                GetAnimator();
        
            _Animator.Update(_DeltaTime);
        }

        void GetAnimator()
        {
            if (GetComponent<Animator>() == null) {
                _Animator = gameObject.AddComponent<Animator>();
                return;
            }

            _Animator = GetComponent<Animator>();
        }
    }
}
