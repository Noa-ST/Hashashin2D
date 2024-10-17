using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehavior : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnstateEnter, updateOnStateExit;
    public bool updateOnstateMachineEnter, updateOnStateMachineExit;
    public float valueOnEnter, valueOnExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnstateEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnstateMachineEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }
}
