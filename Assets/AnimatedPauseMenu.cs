using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class AnimatedPauseMenu : MonoBehaviour
{
    public CanvasGroup pauseMenuUI; // Use CanvasGroup to control fade
    public float fadeDuration = 0.5f;
    private bool isPaused = false;

    void Start()
    {
        // Lock and hide the cursor at game start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                StartCoroutine(FadeOutPauseMenu());
            else
                StartCoroutine(FadeInPauseMenu());
        }
    }

    IEnumerator FadeInPauseMenu()
    {
        pauseMenuUI.gameObject.SetActive(true);

        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f; // Freeze the game
        isPaused = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            pauseMenuUI.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        pauseMenuUI.alpha = 1f;
    }

    IEnumerator FadeOutPauseMenu()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f; // Resume the game
        isPaused = false;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            pauseMenuUI.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        pauseMenuUI.alpha = 0f;
        pauseMenuUI.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        StartCoroutine(FadeOutPauseMenu());

        // Optional: lock cursor on resume from button
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // MusicManager.Instance.PlayMusic("Game");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
