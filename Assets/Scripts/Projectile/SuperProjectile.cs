using UnityEngine;

public class SuperProjectile : MonoBehaviour
{
    [SerializeField] private float timer = 5;

    private void Start()
    {
        if(gameObject != null)
        Destroy(gameObject, timer);
    }
}
