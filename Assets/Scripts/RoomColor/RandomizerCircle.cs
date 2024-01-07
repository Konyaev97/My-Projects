using UnityEngine;

public class RandomizerCircle : MonoBehaviour
{
    public Transform spawnPoint;
    public float minForce = 5f;
    public float maxForce = 10f;

    private Rigidbody2D rb;
    private bool isActivated = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ActivateObject();
    }

    public void TriggerObject()
    {
        DeactivateObject();
        ActivateObject();
    }

    private void ActivateObject()
    {
        if (isActivated)
            return;

        isActivated = true;
        transform.position = spawnPoint.position;
        gameObject.SetActive(true);

        float angle = Random.Range(-70f, 70f);
        float force = Random.Range(minForce, maxForce);

        Quaternion localRotation = Quaternion.Euler(0f, 0f, angle);
        Vector2 direction = transform.localRotation * localRotation * Vector2.up;

        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void DeactivateObject()
    {
        isActivated = false;
        gameObject.SetActive(false);
    }
}
