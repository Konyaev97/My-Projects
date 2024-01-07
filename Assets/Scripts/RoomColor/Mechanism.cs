using UnityEngine;

public class Mechanism : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHeight;
    [SerializeField] private float dropHeight;
    private Vector2 targetPosition;

    private void Start()
    {
        targetPosition = new Vector2(transform.localPosition.x, dropHeight);
    }

    private void Update()
    {
        Vector2 currentPosition = transform.localPosition;
        currentPosition.y += speed * Time.deltaTime;

        transform.localPosition = currentPosition;

        if (currentPosition.y >= maxHeight)
        {
            transform.localPosition = targetPosition;
        }
    }
}
