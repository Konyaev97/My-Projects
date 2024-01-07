using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleActivation : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToActivate;
    [SerializeField] private float activationDelay = 1f;


    private void Start()
    {
        if (objectsToActivate.Count > 0)
        {
            StartCoroutine(ActivateObjectsSequentially());
        }
    }

    private IEnumerator ActivateObjectsSequentially()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(activationDelay);
        }

    }
}
