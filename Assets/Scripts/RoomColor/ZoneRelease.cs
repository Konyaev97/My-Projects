using System.Collections.Generic;
using UnityEngine;

public class ZoneRelease : MonoBehaviour
{
    [SerializeField] private ZoneX2 zoneX2;
    [SerializeField] private Color zoneColor;
    [SerializeField] private int index;

    [Header("SwapPanels")]
    [SerializeField] private Transform mainPanel;
    [SerializeField] private Transform otherPanel;
    [SerializeField] private GameObject mainSpawnPoint;    
    [SerializeField] private GameObject otherSpawnPoint;   

    [SerializeField] private bool swapPanel;           // �� ��������  false , � �� �������� true
    private bool canAcceptShots = true;     

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RandomizerCircle circle))
        {
            circle.TriggerObject();

            if ((zoneX2.currentCount < 64 || swapPanel) && canAcceptShots)
            {
                canAcceptShots = false;
                Cannon[] cannons = FindCannonsByColor(zoneColor);

                foreach (Cannon cannon in cannons)
                {
                    cannon.Shoot(zoneX2.currentCount, index);
                }

                if (swapPanel)
                {
                    SwapPanel(true);
                }

                ShootFinished();
            }
            else if (!swapPanel)
            {
                SwapPanel(false);
                return;
            }
        }
    }

    private void ShootFinished()
    {
        canAcceptShots = true;
        zoneX2.ResetShotCount();
    }

    private void SwapPanel(bool spawnPoint)
    {
        canAcceptShots = true;
        Vector2 currentPosition = mainPanel.transform.position;
        Vector2 otherPanelPosirion = otherPanel.transform.position;

        mainPanel.transform.position = otherPanelPosirion;
        otherPanel.transform.position = currentPosition;

        mainSpawnPoint.SetActive(spawnPoint);
        otherSpawnPoint.SetActive(!spawnPoint);
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
}
