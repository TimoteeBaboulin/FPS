using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.Playables;

/*public class IKConstraintsEditMode : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint[] _Constraints;
    [SerializeField] [Range(0, 1)] private float _DeltaTime = 0;
    private IAnimationJob[] _Job = new IAnimationJob[10];
    
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        
        for (int x = 0; x < _Constraints.Length; x++)
        {
            if (_Constraints[x] == null)
                return;

            if (_Job[x] == null) {
                _Job[x] = _Constraints[x].CreateJob(animator);
            }
            
            _Constraints[x].UpdateJob(_Job[x]);

            PlayableGraph graph = animator.playableGraph;
            
            
            
            _Job[x].ProcessRootMotion(new AnimationStream());
            _Job[x].ProcessAnimation(new AnimationStream());
        }
    }
}*/
