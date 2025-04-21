using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public int buttonValue; // Assign 1-9 in the Inspector
    public delegate void ButtonPressEvent(int value);
    public static event ButtonPressEvent OnButtonPressed;

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton2))// Trigger the button press event
        {OnButtonPressed?.Invoke(buttonValue);}
    }
}
