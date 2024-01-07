using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    [SerializeField] private float startAngle = 0f; 
    [SerializeField] private float endAngle = 180f; 
    [SerializeField] private float rotationTime = 4f; 

    private float rotationTimer; 

    private void Start()
    {
        rotationTimer = 0f;
    }

    private void LateUpdate()
    {
        RotateCannon();
    }

    private void RotateCannon()
    {
        rotationTimer += Time.deltaTime;

        float t = rotationTimer / rotationTime;
        t = Mathf.Clamp01(t); // Нормализуем таймер в диапазоне от 0 до 1

        float targetAngle = Mathf.Lerp(startAngle, endAngle, t);

        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);

        if (rotationTimer >= rotationTime)
        {
            // Меняем начальный и конечный углы для обратного вращения
            float temp = startAngle;
            startAngle = endAngle;
            endAngle = temp;

            rotationTimer = 0f;
        }
    }
}
