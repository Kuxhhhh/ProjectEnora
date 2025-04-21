using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnPlay(){
        SceneManager.LoadScene("Lore");
        MusicManager.Instance.PlayMusic("Level1");
    }
    public void OnQuit(){
        print("Quitting Application");
        Application.Quit();
    }
}
