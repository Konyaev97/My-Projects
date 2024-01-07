using UnityEngine;

public class AnimBlockCannon : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetBool("isBlock", true);
    }

    private void OnDisable()
    {
        animator.SetBool("isBlock", false);
    }
}
