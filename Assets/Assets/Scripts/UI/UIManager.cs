using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake() {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region  GameOver
    public void GameOver(){
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void Quit(){
        Application.Quit();

        // ONLY IN EDITOR
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status) {
        pauseScreen.SetActive(status);
        // scale time to 0 to stop the game
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void soundVolume(){
        SoundManager.instance.ChangeSoundVolume(0.1f);
    }

    public void MusicVolume(){
        SoundManager.instance.ChangeMusicVolume(0.1f);
    }
    #endregion
}