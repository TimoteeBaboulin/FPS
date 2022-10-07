using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class TemporaryIK : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = true;
    public Transform RightHandObj = null;
    public Transform RightHandHint = null;
    public Transform LeftHandObj = null;
    public Transform LeftHandHint = null;

    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {

            //if the IK is active, set the position and rotation directly to the goal.
            if(ikActive) {

                // Set the right hand target position and rotation, if one has been assigned
                if(RightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,RightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,RightHandObj.rotation);
                }
                
                if(RightHandHint != null) {
                    animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow,1);
                    animator.SetIKHintPosition(AvatarIKHint.RightElbow,RightHandHint.position);
                }
                
                if(LeftHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.LeftHand,LeftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand,LeftHandObj.rotation);
                }
                
                if(LeftHandHint != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand,LeftHandObj.position);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0);
                animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0);
                animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }    
}
