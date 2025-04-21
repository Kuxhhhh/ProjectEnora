using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // You can change this in the Inspector if needed
    public KeyCode startKey = KeyCode.Return; // 'Return' is the Enter key

    void Update()
    {
        if (Input.GetKeyDown(startKey))
        {
            Debug.Log("Enter key pressed. Loading Dungeon1...");
            SceneManager.LoadScene("Dungeon1");
        }
    }
}
