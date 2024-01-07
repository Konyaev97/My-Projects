using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public Color ColorObject; // ÷вет после столкновени€ с другим снар€дом
    public bool isAlive = true;

    [SerializeField] private float impulseForce = 0.5f;
    [SerializeField] private float maxImpulseForce = 5;

    private Renderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive)
            return;

        GameObject otherObject = collision.gameObject;

        if (otherObject.GetComponent<ProjectileCollision>() != null)
        {
            if (otherObject.CompareTag("Virus"))
            {
                return;
            }

            Renderer otherRenderer = otherObject.GetComponent<Renderer>();
            if (otherRenderer.material.color != ColorObject)
            {
                otherRenderer.material.color = spriteRenderer.material.color;
            }

            AddForceInpulse(collision);
        }
    }

    private void AddForceInpulse(Collision2D collision)
    {
        Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if (otherRigidbody.velocity.sqrMagnitude >= maxImpulseForce * maxImpulseForce)
        {
            otherRigidbody.velocity = Vector2.zero;
            return;
        }

        otherRigidbody.AddForce(collision.relativeVelocity * impulseForce, ForceMode2D.Impulse);
    }
}
