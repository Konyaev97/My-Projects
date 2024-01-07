using System.Collections.Generic;
using UnityEngine;

public class ProjectPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private int[] poolSizes;

    private List<GameObject>[] projectilePools;

    private void Awake()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        projectilePools = new List<GameObject>[projectilePrefabs.Length];

        for (int i = 0; i < projectilePrefabs.Length; i++)
        {
            projectilePools[i] = new List<GameObject>();

            for (int j = 0; j < poolSizes[i]; j++)
            {
                GameObject projectile = Instantiate(projectilePrefabs[i]);
                projectile.SetActive(false);
                projectile.transform.SetParent(transform);
                projectilePools[i].Add(projectile);
            }
        }
    }

    public GameObject GetProjectile(int type)
    {
        if (type < 0 || type >= projectilePools.Length)
        {
            return null; // �������� ��� �������
        }

        foreach (GameObject projectile in projectilePools[type])
        {
            if (projectile != null && !projectile.activeSelf)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }

        return null; // ���� ���� ��� �����
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
    }
}
