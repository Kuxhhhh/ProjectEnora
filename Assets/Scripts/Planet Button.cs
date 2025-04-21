using UnityEngine;

public class PlanetButton : MonoBehaviour
{
    public string buttonValue; // Assign 1-9 in the Inspector
    public delegate void ButtonPressEvent(string value);
    public static event ButtonPressEvent OnButtonPressed;

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton2))// Trigger the button press event
        {OnButtonPressed?.Invoke(buttonValue);}
    }
}
