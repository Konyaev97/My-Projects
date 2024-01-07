using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Color[] colors;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void AreAllCannonsSameColor()
    {
        Cannon[] allCannons = FindObjectsOfType<Cannon>(); // Находим все пушки в сцене

        for (int i = 0; i < colors.Length; i++)
        {
            for (int j = 0; j < allCannons.Length; j++)
            {
                if (colors[i] == allCannons[j].cannonColor)
                {
                    ShowRestartButton();
                }
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowRestartButton()
    {
        gameObject.SetActive(true);
    }
}
