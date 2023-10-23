using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        FindObjectOfType<GameManager>().OnPointUpdate += PlayCollectAnimation;
    }

    private void PlayCollectAnimation()
    {
        animator.SetTrigger("Collect");
    }
}
