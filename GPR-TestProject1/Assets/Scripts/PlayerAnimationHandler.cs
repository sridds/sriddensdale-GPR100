using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private TopDownMovement movement;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        FindObjectOfType<GameManager>().OnPointUpdate += PlayCollectAnimation;
    }

    private void Update()
    {
        if (movement.IsMoving)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void PlayCollectAnimation()
    {
        animator.SetTrigger("Collect");
    }
}
