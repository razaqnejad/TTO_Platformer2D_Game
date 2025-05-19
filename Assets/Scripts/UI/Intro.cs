using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup; // For fading the entire canvas
    [SerializeField] private CanvasGroup textGroup; // For fading text
    [SerializeField] private Text storyText; // The text component
    [SerializeField] private string[] storyLines; // Array of story text lines
    [SerializeField] private float textDisplayTime = 2f; // Time each text is displayed
    [SerializeField] private float fadeTime = 1f; // Time for text fade in/out
    [SerializeField] private string firstLevelSceneName = "Level 1"; // Name of the first level scene

    private int currentLineIndex = 0;

    void Start()
    {
        // Ensure canvas and text are fully visible at start
        canvasGroup.alpha = 1f;
        textGroup.alpha = 0f;
        StartCoroutine(PlayIntroSequence());
    }

    void Update()
    {
        // Check for Escape key to skip intro
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    private IEnumerator PlayIntroSequence()
    {
        // Loop through each story line
        foreach (string line in storyLines)
        {
            storyText.text = line;

            // Fade in text
            yield return StartCoroutine(FadeCanvasGroup(textGroup, 0f, 1f, fadeTime));
            // Display text for specified time
            yield return new WaitForSeconds(textDisplayTime);
            // Fade out text
            yield return StartCoroutine(FadeCanvasGroup(textGroup, 1f, 0f, fadeTime));
        }

        // After all text, fade out entire canvas and load first level
        yield return StartCoroutine(FadeOutAndLoadScene());
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

    private IEnumerator FadeOutAndLoadScene()
    {
        // Fade out entire canvas
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, fadeTime));
        // Load the first level scene
        SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Single);
    }
}