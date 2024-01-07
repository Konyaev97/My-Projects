using UnityEngine;

public class AnimSurge : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetBool("isSurge", true);
    }

    private void OnDisable()
    {
        animator.SetBool("isSurge", false);
    }
}
