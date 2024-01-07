using UnityEngine;
using System.Collections.Generic;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private Color targetColor; 
    [SerializeField] private GameObject[] objectList; // Массив объектов для выключения
    [SerializeField] private GameObject winnerObject; // Рамка
    [SerializeField] private GameObject[] objectListCircle;
    [SerializeField] private GameObject targetPos;

    [SerializeField] private UIManager manager;

    private int objectCount; // Текущее количество объектов заданного цвета
    private bool isLoser = true;

    private void Awake()
    {
        // Инициализация количества объектов заданного цвета
        objectCount = CountObjectsWithTargetColor();
    }

    private void LateUpdate()
    {
        if (isLoser)
        {
            objectCount = CountObjectsWithTargetColor();

            if (objectCount <= 0)
            {
                // Выключаем объекты из objectList
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
        Cannon[] cannons = FindCannonsByColor(targetColor); // Находим пушки с заданным цветом зоны
        return cannons.Length;
    }

    private Cannon[] FindCannonsByColor(Color color)
    {
        Cannon[] allCannons = FindObjectsOfType<Cannon>(); // Находим все пушки в сцене
        List<Cannon> matchingCannons = new List<Cannon>(); // Список для хранения пушек с заданным цветом

        foreach (Cannon cannon in allCannons)
        {
            if (cannon.cannonColor == color) // Проверяем цвет пушки
            {
                matchingCannons.Add(cannon); // Добавляем пушку в список совпадающих пушек
            }
        }

        return matchingCannons.ToArray(); // Возвращаем массив пушек с заданным цветом
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
