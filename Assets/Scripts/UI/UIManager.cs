using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void PlaySceneTransition(Animator sceneTransitionAnimator, string animationState, bool reversed)
    {
        sceneTransitionAnimator.Play(animationState, 0, reversed ? 1 : 0);
    }
}
