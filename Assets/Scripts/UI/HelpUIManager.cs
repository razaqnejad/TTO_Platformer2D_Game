using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    [SerializeField] private RectTransform[] buttons;
    [SerializeField] private AudioClip interactSound;
    private int currentPosition;

    private void Awake()
    {
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit")){
            SoundManager.instance.PlaySound(interactSound);
            SceneManager.LoadSceneAsync("_MainMenu", LoadSceneMode.Single);
        }
    }
}