using UnityEngine;

public class EmblemaRotation : MonoBehaviour
{
    [SerializeField] private float speed;
 
    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 1f * speed);
        transform.rotation *= rotation;
    }
}
