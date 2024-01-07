using UnityEngine;
using TMPro;

public class ZoneX2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    public int currentCount = 1;
    private int countStart = 1;

    private void Awake()
    {
        textMeshProUGUI.text = countStart.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RandomizerCircle Circle))
        {
            Circle.TriggerObject();

            if (currentCount != 1024)
            {
                 currentCount *= 2;
            }

            NextTextUI();
        }
    }

    public void ResetShotCount()
    {
        currentCount = countStart; // —брасываем число снар€дов
        NextTextUI();
    }

    private void NextTextUI()
    {
        if (currentCount != 1024)
        {
            textMeshProUGUI.text = currentCount.ToString();
        }
        else
        {
            textMeshProUGUI.text = $"{currentCount}\nLimit";
        }
    }
}
