using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    [SerializeField] private RectTransform[] buttons;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private GameData data;
    private int currentPosition;

    private void Awake()
    {
        ChangePosition(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
            Interact();
    }

    public void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = buttons.Length - 1;
        else if (currentPosition > buttons.Length - 1)
            currentPosition = 0;

        AssignPosition();
    }
    private void AssignPosition()
    {
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        if (currentPosition == 0)
        {
            // SceneManager.LoadScene(2);
            DataPersistenceManager.instance.NewGame();
            SceneManager.LoadSceneAsync("Level 1", LoadSceneMode.Single);
        }
        else if (currentPosition == 1)
        {
            SceneManager.LoadSceneAsync("_Levels"); // Locations
            // SceneManager.LoadScene(data.buildIndex);
        }
        else if (currentPosition == 2)
        {
            SceneManager.LoadSceneAsync("_Help"); // Levels
            // SceneManager.LoadScene(data.buildIndex);
        }
        else if (currentPosition == 3)
            Application.Quit();
    }
}