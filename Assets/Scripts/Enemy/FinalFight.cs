using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalFight : MonoBehaviour
{
    [SerializeField] private Health enemyHealth; // Reference to the enemy's Health component
    [SerializeField] private CanvasGroup gameWinCanvas; // Reference to the Game Win UI CanvasGroup
    [SerializeField] private GameObject[] enemiesToDestroy; // Array of additional enemies to destroy
    [SerializeField] private float fadeTime = 1f; // Time for fading in the UI
    [SerializeField] private float displayTime = 3f; // Time to display the UI before returning to main menu
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Name of the main menu scene

    private bool hasTriggered = false; // Prevent multiple triggers

    private void Awake()
    {
        // Ensure the Game Win UI is hidden at start
        if (gameWinCanvas != null)
        {
            gameWinCanvas.alpha = 0f;
            gameWinCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the enemy is dead and hasn't triggered the win sequence yet
        if (!hasTriggered && enemyHealth != null && enemyHealth.currentHealth <= 0)
        {
            hasTriggered = true;
            // Destroy additional enemies
            foreach (GameObject enemy in enemiesToDestroy)
            {
                if (enemy != null)
                {
                    Destroy(enemy);
                }
            }
            StartCoroutine(ShowGameWinSequence());
        }
    }

    private IEnumerator ShowGameWinSequence()
    {
        // Activate and fade in the Game Win UI
        if (gameWinCanvas != null)
        {
            gameWinCanvas.gameObject.SetActive(true);
            yield return StartCoroutine(FadeCanvasGroup(gameWinCanvas, 0f, 1f, fadeTime));
        }

        // Wait for the display time
        yield return new WaitForSeconds(displayTime);

        // Fade out the UI
        if (gameWinCanvas != null)
        {
            yield return StartCoroutine(FadeCanvasGroup(gameWinCanvas, 1f, 0f, fadeTime));
        }

        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup group, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        group.alpha = endAlpha;
    }
}