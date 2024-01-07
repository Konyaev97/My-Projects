using UnityEngine;
using System.Collections.Generic;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private Color targetColor; 
    [SerializeField] private GameObject[] objectList; // ������ �������� ��� ����������
    [SerializeField] private GameObject winnerObject; // �����
    [SerializeField] private GameObject[] objectListCircle;
    [SerializeField] private GameObject targetPos;

    [SerializeField] private UIManager manager;

    private int objectCount; // ������� ���������� �������� ��������� �����
    private bool isLoser = true;

    private void Awake()
    {
        // ������������� ���������� �������� ��������� �����
        objectCount = CountObjectsWithTargetColor();
    }

    private void LateUpdate()
    {
        if (isLoser)
        {
            objectCount = CountObjectsWithTargetColor();

            if (objectCount <= 0)
            {
                // ��������� ������� �� objectList
                foreach (GameObject obj in objectList)
                {
                    if (obj.activeSelf)
                    {
                        obj.SetActive(false);
                    }
                }

                if (!winnerObject.activeSelf)
                {
                    winnerObject.SetActive(true);
                }

                UpdatePosCircle();

                //manager.AreAllCannonsSameColor(); TODO
                isLoser = false;
            }
        }
    }

    private int CountObjectsWithTargetColor()
    {
        Cannon[] cannons = FindCannonsByColor(targetColor); // ������� ����� � �������� ������ ����
        return cannons.Length;
    }

    private Cannon[] FindCannonsByColor(Color color)
    {
        Cannon[] allCannons = FindObjectsOfType<Cannon>(); // ������� ��� ����� � �����
        List<Cannon> matchingCannons = new List<Cannon>(); // ������ ��� �������� ����� � �������� ������

        foreach (Cannon cannon in allCannons)
        {
            if (cannon.cannonColor == color) // ��������� ���� �����
            {
                matchingCannons.Add(cannon); // ��������� ����� � ������ ����������� �����
            }
        }

        return matchingCannons.ToArray(); // ���������� ������ ����� � �������� ������
    }

    private void UpdatePosCircle()
    {
        Vector3 targetPosition = targetPos.transform.position;

        for (int i = 0; i < objectListCircle.Length; i++)
        {
            objectListCircle[i].transform.position = targetPosition;
        }
    }
}
