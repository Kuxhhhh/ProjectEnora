using UnityEngine;
using UnityEngine.SceneManagement;

public class Fireball : MonoBehaviour
{
    private Vector3 movementDirection;
    
    public void Initialize(Vector3 direction)
    {
        movementDirection = direction;
    }

    private void Update()
    {
        if (movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection);
        }
    }

   private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Cannon")){
        Debug.Log("Hit The Player!!!");
        SceneManager.LoadScene("Game");
       }
    
}}