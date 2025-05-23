using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    // [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private int level;
    [SerializeField] private CanvasGroup fadeCanvasGroup; // Reference to the CanvasGroup for fading
    [SerializeField] private float fadeDuration = 1f; // Duration of fade in seconds

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                // cam.MoveToNewRoom(nextRoom);
                // nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
                StartCoroutine(FadeAndLoadScene(level + 4));
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                // nextRoom.GetComponent<Room>().ActivateRoom(false);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
                StartCoroutine(FadeAndLoadScene(level + 2));
            }
        }
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        // Fade out to black
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Load the new scene
        SceneManager.LoadScene(sceneIndex);

        // Note: Fade-in will be handled in the new scene (see below)
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;
    }
}