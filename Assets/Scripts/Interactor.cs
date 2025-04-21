using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public LayerMask InteractableLayermask;
    UnityEvent onInteract;
    public CrosshairController crosshairController; // Reference to the CrosshairController script

    void Start()
    {
        if (crosshairController == null)
        {
            crosshairController = FindObjectOfType<CrosshairController>();
        }
    }

    void Update()
    {
        RaycastHit hit;
        bool isLookingAtInteractable = false;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, InteractableLayermask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                isLookingAtInteractable = true; // The player is looking at an interactable object
                onInteract = interactable.onInteract;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    onInteract.Invoke();
                    SoundManager.Instance.PlaySound2D("Interact");
                }
            }
        }

        // Update crosshair color based on interactable state
        if (crosshairController != null)
        {
            crosshairController.SetCrosshairState(isLookingAtInteractable);
        }
    }
}
