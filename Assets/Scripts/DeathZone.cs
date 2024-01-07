using TMPro;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private int maxHits = 100;
    [SerializeField] private TextMeshProUGUI counterText;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ProjectileCollision>(out var projectileCollision) 
            && projectileCollision.isAlive)
        {
            projectileCollision.isAlive = false;

            Destroy(collision.gameObject);

            maxHits--;
            counterText.text = maxHits.ToString();

            if (maxHits <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
