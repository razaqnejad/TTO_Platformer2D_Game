using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake() { // Hide game over and pause screen
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update() { // Listen to escape key
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region  GameOver
    public void GameOver(){ // Show game over screen
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart(){ // Restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
 
    #region Pause
    public void PauseGame(bool status) { // Pause the game
        pauseScreen.SetActive(status);
        // scale time to 0 to stop the game
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void soundVolume(){ // Change sound effects volume
        SoundManager.instance.ChangeSoundVolume(0.1f);
    }

    public void MusicVolume(){ // Change music volume
        SoundManager.instance.ChangeMusicVolume(0.1f);
    }
    #endregion

    public void MainMenu(){ // Go to main menu
        SceneManager.LoadSceneAsync("_MainMenu");
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // if (player != null) {
        //     Destroy(player);
        // }
    }
    public void Quit(){ // Quit the game
        Application.Quit();

        // ONLY IN EDITOR
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}