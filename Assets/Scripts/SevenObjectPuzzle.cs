using UnityEngine;
using System.Collections;

public class SevenObjectPuzzle : MonoBehaviour
{
    public GameObject[] interactableObjects; // Assign the 7 objects here
    public int[] correctOrder = { 0, 1, 2, 3, 4, 5, 6 }; // Correct interaction order (indexes)
    private int currentStep = 0;
    private Vector3[] originalPositions;
private Vector3[] originalScales;


    public GameObject door; // The door to open
    private Animator doorAnimator;
    public string doorOpenTrigger = "Open"; // Trigger to open door

    private Animator[] objectAnimators;

    void Start()
    {
        if (door != null)
        doorAnimator = door.GetComponent<Animator>();

    objectAnimators = new Animator[interactableObjects.Length];
    originalPositions = new Vector3[interactableObjects.Length];
    originalScales = new Vector3[interactableObjects.Length];

    for (int i = 0; i < interactableObjects.Length; i++)
    {
        if (interactableObjects[i] != null)
        {
            objectAnimators[i] = interactableObjects[i].GetComponent<Animator>();
            originalPositions[i] = interactableObjects[i].transform.localPosition;
            originalScales[i] = interactableObjects[i].transform.localScale;
        }
        else
        {
            Debug.LogWarning("Missing object in interactableObjects at index: " + i);
        }
    }
    }
    public void ResetPuzzleManually()
{
    Debug.Log("Manual Reset Called!");

    for (int i = 0; i < interactableObjects.Length; i++)
    {
        if (objectAnimators[i] != null)
        {
            objectAnimators[i].Play("Idle"); // Reset animation state to Idle
        }

        interactableObjects[i].transform.localPosition = originalPositions[i]; // Reset position
        interactableObjects[i].transform.localScale = originalScales[i];       // Reset scale
    }

    currentStep = 0;
}


    public void InteractWithObject(GameObject obj)
    {
        int index = System.Array.IndexOf(interactableObjects, obj);

        if (index == -1)
        {
            Debug.LogWarning("Interacted object not part of puzzle!");
            return;
        }

        Debug.Log("Interacted with object: " + obj.name + " at step " + currentStep);

        if (index == correctOrder[currentStep])
        {
            // Correct interaction
            Animator anim = obj.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Godown"); // Play Godown trigger
            }

            currentStep++;

            if (currentStep == correctOrder.Length)
            {
                OpenDoor();
                SoundManager.Instance.PlaySound2D("MetalDoor");
            }
        }
        else
        {
            Debug.Log("Wrong order! Resetting puzzle.");
            StartCoroutine(ResetAnimationsAndPuzzle());
        }
    }

    IEnumerator ResetAnimationsAndPuzzle()
    {
        // Reset all objects (send them back up)
        foreach (Animator anim in objectAnimators)
        {
            if (anim != null)
            {
                anim.SetTrigger("GoUp"); // NEW: trigger the "GoUp" animation
            }
        }

        yield return new WaitForSeconds(0.5f);
        currentStep = 0;
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(doorOpenTrigger);
            Debug.Log("Door opened with animation!");
        }
        else if (door != null)
        {
            door.SetActive(false);
            Debug.Log("Door deactivated!");
        }
    }
}
