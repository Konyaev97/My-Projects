using UnityEngine;

public class DeathIsOutOfPlay : MonoBehaviour
{
    [SerializeField] private ProjectPoolManager projectilePoolManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ProjectileCollision>(out var projectileCollision))
        {
            projectilePoolManager.ReturnProjectile(collision.gameObject);
        }
    }
}
