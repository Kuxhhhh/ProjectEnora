using UnityEngine;
using UnityEngine.SceneManagement;

public class level2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            SceneManager.LoadScene("game");
            MusicManager.Instance.PlayMusic("Level2");
        }
    }
}
