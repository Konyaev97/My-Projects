using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public Color ColorObject; // ÷вет после столкновени€ с другим снар€дом

    private Renderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.GetComponent<ProjectileCollision>() != null)
        {
            if (otherObject.CompareTag("Rocket") || otherObject.CompareTag("Virus"))
            {
                return;
            }

            Renderer otherRenderer = otherObject.GetComponent<Renderer>();
            if (otherRenderer.material.color != ColorObject)
            {
                otherRenderer.material.color = spriteRenderer.material.color;
            }
        }
    }
}
